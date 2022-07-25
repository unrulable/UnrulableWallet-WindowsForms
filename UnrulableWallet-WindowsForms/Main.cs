using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static UnrulableWallet.UI.Shared.Enums;
using NBitcoin;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HBitcoin.KeyManagement;
using Newtonsoft.Json.Linq;
using QBitNinja.Client;
using UnrulableWallet.UI.Models;
using static UnrulableWallet.UI.Wrapper.QBitNinjaWrapper;
using static UnrulableWallet.UI.Shared.Helpers;
using WalletModel = UnrulableWallet.UI.Models.WalletModel;

namespace UnrulableWallet.UI
{
    public partial class Main : Form
    {
        #region Fields

        private ReceiveBitcoinModel _receiveBitcoinModel;
        private WalletModel _walletModel;

        #endregion
        #region Properties
        public int CurrentAvailableWalletsCount => this.comboBoxYourAvailableWallets.Items.Count;
        public string CurrentOpenedWalletName => this.comboBoxYourAvailableWallets.SelectedItem.ToString();

        #endregion

        #region Constructors
        public Main()
        {
            InitializeComponent();

            // Load config file
            Config.Load();

            // Load available wallets to open
            _walletModel = new WalletModel();
            GetAvailableBitcoinWallets();

            // Set the data bindings
            comboBoxYourAvailableWallets.DataSource = _walletModel.AvailableWallets;
            walletModelBindingSource.DataSource = _walletModel;
        }
        #endregion

        #region Tasks

        private async void StartTask(object method)
        {
            //await Task.Run(GetAvailableBitcoinWallets());
        }

        #endregion

        #region Display Methods

        private void PopulateAvailableWalletsDropDown(List<string> walletsList)
        {
            comboBoxYourAvailableWallets.Items.Clear();
            comboBoxYourAvailableWallets.Items.AddRange(walletsList.ToArray());
        }

        private void GenerateTransactionDetailsDisplay(GetTransactionResponse transaction)
        {
            var confirmationsCount = transaction.Block?.Confirmations != null
                ? transaction.Block.Confirmations.ToString()
                : "0";
            lblTransactionStatusValue.Text = confirmationsCount;
            lblIncludedInBlockValue.Text = $"{transaction.Block.BlockId}";
            lblBlockHeightValue.Text = $"{transaction.Block.Height}";
            lblBlockTimestampValue.Text = $"{transaction.Block.BlockTime}";
            lblVersionValue.Text = $"{transaction.Block.BlockHeader.Version}";
        }

        private void GenerateWalletRecoveredDisplay(Mnemonic mnemonic, string walletFilePath)
        {
            //groupBoxNewWalletDetails.Show();
            //lblMnemonicValue.Text = mnemonic.ToString();
            //lblWalletFilePathValue.Text = walletFilePath;
            //lblWalletCreateStatusValue.Text = "Successful";
            //lblWalletCreateStatusValue.ForeColor = Color.Green;
        }

        private void GenerateQrCodeDisplay(string value, int qrCodeHeight, PictureBox pictureBoxObject)
        {
            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pictureBoxObject.Image = qrcode.Draw(value, qrCodeHeight);
        }

        #endregion

        #region Click Events

        private void comboBoxAvailableWallets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            string selectedValue = (string)cmb.SelectedValue;
            _walletModel.CurrentOpenedWalletName = selectedValue;
        }

        private void btnSearchTransactionId_Click(object sender, EventArgs e)
        {
            string transactionId = txtSearchTransactionId.Text;
            // AssertArgumentsLength(args.Length, 1, 2);
            // var walletFilePath = GetWalletFilePath(args);
            // Safe safe = DecryptWalletByAskingForPassword(walletFilePath);

            if (Config.ConnectionType == ConnectionType.Http)
            {
                //Query the transaction id
                GetTransactionResponse transaction = QueryTransactionId(transactionId);
                GenerateTransactionDetailsDisplay(transaction);
            }
            else if (Config.ConnectionType == ConnectionType.FullNode)
            {
                //NetworkCredential creds = new NetworkCredential()
                //{
                //    UserName = "bitcoin",
                //    Password = "test1234"
                //};
                //RPCClient client = new RPCClient(creds, "localhost", Network.Main);
                //uint256 parsedTransactionId = uint256.Parse("f1db4031c582bbf71b7515dc444e1725884b4fed0f71bac2519382a7d666e543");
                //Transaction transactionInfo = client.GetRawTransaction(parsedTransactionId);
                //WriteLine();
                //WriteLine("---------------------------------------------------------------------------");
                //WriteLine("Transaction Id Details: ");
                //WriteLine("---------------------------------------------------------------------------");
                //WriteLine("---------------------------------------------------------------------------");
                ////WriteLine($"Transaction Id: {transactionInfo.} ");
                ////WriteLine($"Transaction: {transactionInfo.Transaction}");
                //WriteLine("---------------------------------------------------------------------------");
            }
            else
            {
                throw new Exception("Invalid connection type.");
            }
        }

        private void btnOpenWallet_Click(object sender, EventArgs e)
        {
            lblStatusRetrievingWalletDetails.Show();
            var walletFilePath = GetWalletFilePath(CurrentOpenedWalletName);
            Safe safe = DecryptWalletByAskingForPassword(walletFilePath, txtOpenWalletPassword.Text);
            List<string> confirmBalances = ShowBalances(walletFilePath, safe);

            tabControlaUnrulableWallet.Show();
            groupBoxEntryView.Hide();
            lblOpenWalletBalanceValue.Text = confirmBalances.Count == 0 ? "0" : CalculateBalanceTotal(confirmBalances);
            lblTransactionCountValue.Text = confirmBalances.Count.ToString();
        }

        private void toolStripMenuItemWalletCreate_Click(object sender, EventArgs e)
        {
            CreateWallet createWalletForm = new CreateWallet();
            createWalletForm.ShowDialog();

            //Refresh available wallets to open list
            //PopulateAvailableWalletsDropDown(GetAvailableBitcoinWallets());
        }

        private void toolStripMenuItemRecoverWallet_Click(object sender, EventArgs e)
        {
            RecoverWallet recoverWalletForm = new RecoverWallet();
            recoverWalletForm.ShowDialog();
        }

        private void toolStripMenuItemOpenWallet_Click(object sender, EventArgs e)
        {
            groupBoxEntryView.Show();
        }

        private void listViewAvailableBitcoinAddress_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxAllAvailableBitcoinAddresses?.SelectedItem == null) return;

            // Get the currently selected item in the ListBox.
            string currentSelectedBitcoinAddressFromList = listBoxAllAvailableBitcoinAddresses.SelectedItem.ToString();
            _receiveBitcoinModel.BitcoinAddress = currentSelectedBitcoinAddressFromList;
            txtCurrentBitcoinReceiveAddress.Text = currentSelectedBitcoinAddressFromList;
            receiveBitcoinModelBindingSource.DataSource = _receiveBitcoinModel;
            GenerateQrCodeDisplay(currentSelectedBitcoinAddressFromList, 25, pictureBoxBitcoinReceiveAddressQrCode);
        }

        private void tabPageControl_IndexChanged(object sender, EventArgs e)
        {
            switch (tabControlaUnrulableWallet.SelectedTab.Text)
            {
                case "Receive":
                    var walletFilePath = GetWalletFilePath(CurrentOpenedWalletName);
                    Safe safe = DecryptWalletByAskingForPassword(walletFilePath, txtOpenWalletPassword.Text);

                    if (Config.ConnectionType == ConnectionType.Http)
                    {
                        Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerReceiveAddresses = QueryOperationsPerSafeAddresses(safe, 7, HdPathType.Receive);

                        BitcoinAddress retrievedAddressInfo = GetCurrentBitcoinAddressFromAvailableAddresses(0, operationsPerReceiveAddresses);

                        _receiveBitcoinModel = new ReceiveBitcoinModel(retrievedAddressInfo, operationsPerReceiveAddresses);
                        receiveBitcoinModelBindingSource.DataSource = _receiveBitcoinModel;
                        GenerateQrCodeDisplay(operationsPerReceiveAddresses.ElementAt(0).Key.ToString(), 25, pictureBoxBitcoinReceiveAddressQrCode);
                    }
                    else if (Config.ConnectionType == ConnectionType.FullNode)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        throw new Exception("Invalid connection type.");
                    }

                    break;
                case "Send":
                    // Test
                    break;
                default:
                    //MessageBox.Show("hello world");
                    break;
            }
        }
        #endregion

        #region Private Methods

        private BitcoinAddress GetCurrentBitcoinAddressFromAvailableAddresses(int index, Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerReceiveAddresses)
        {
            return operationsPerReceiveAddresses.ElementAt(index).Key;
        }

        private string CalculateBalanceTotal(List<string> confirmBalances)
        {
            return "0";
        }

        private void GetAvailableBitcoinWallets()
        {
            string[] wallets = Directory.GetFiles(@"Wallets", "*");
            List<string> walletsList = new List<string>();

            // The stored wallet count != wallets we just pulled from directory
            // then update the list so we have the full list of available wallets
            if (CurrentAvailableWalletsCount != wallets.Length)
            {
                foreach (string wallet in wallets)
                {
                    string fileName = Path.GetFileName(wallet).Replace(".json", string.Empty);
                    walletsList.Add(fileName);
                }
            }

            _walletModel.AvailableWallets = walletsList;
        }

        private static Safe DecryptWalletByAskingForPassword(string walletFilePath, string walletPassword)
        {
            Safe safe = null;
            string pw = walletPassword;
            bool correctPw = false;
            do
            {
                try
                {
                    safe = Safe.Load(pw, walletFilePath);
                    AssertCorrectNetwork(safe.Network);
                    correctPw = true;
                }
                catch (System.Security.SecurityException)
                {
                    correctPw = false;
                    break;
                }
            } while (!correctPw);

            if (safe == null)
                throw new Exception("Wallet could not be decrypted.");

            return safe;
        }

        #endregion

        private void btnSendBitcoin_Click(object sender, EventArgs e)
        {
            var walletFilePath = GetWalletFilePath(_walletModel.CurrentOpenedWalletName);
            BitcoinAddress addressToSend;
            try
            {
                string address = txtSendBitcoinTo.Text;//GetArgumentValue(args, argName: "address", required: true);
                addressToSend = BitcoinAddress.Create(address, Config.Network);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Safe safe = DecryptWalletByAskingForPassword(walletFilePath, txtOpenWalletPassword.Text);

            if (Config.ConnectionType == ConnectionType.Http)
            {
                Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerAddresses = QueryOperationsPerSafeAddresses(safe, 7);

                // 1. Gather all the not empty private keys
                //WriteLine("Finding not empty private keys...");
                var operationsPerNotEmptyPrivateKeys = new Dictionary<BitcoinExtKey, List<BalanceOperation>>();
                foreach (var elem in operationsPerAddresses)
                {
                    var balance = Money.Zero;
                    foreach (var op in elem.Value) balance += op.Amount;
                    if (balance > Money.Zero)
                    {
                        var secret = safe.FindPrivateKey(elem.Key);
                        operationsPerNotEmptyPrivateKeys.Add(secret, elem.Value);
                    }
                }

                // 2. Get the script pubkey of the change.
                //WriteLine("Select change address...");
                Script changeScriptPubKey = null;
                Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerChangeAddresses = QueryOperationsPerSafeAddresses(safe, minUnusedKeys: 1, hdPathType: HdPathType.Change);
                foreach (var elem in operationsPerChangeAddresses)
                {
                    if (elem.Value.Count == 0)
                        changeScriptPubKey = safe.FindPrivateKey(elem.Key).ScriptPubKey;
                }
                if (changeScriptPubKey == null)
                    throw new ArgumentNullException();

                // 3. Gather coins can be spend
                //WriteLine("Gathering unspent coins...");
                Dictionary<Coin, bool> unspentCoins = GetUnspentCoins(operationsPerNotEmptyPrivateKeys.Keys);

                // 4. Get the fee
                //WriteLine("Calculating transaction fee...");
                Money fee;
                try
                {
                    var txSizeInBytes = 250;
                    using var httpClient = new HttpClient();
                    const string request = @"https://bitcoinfees.earn.com/api/v1/fees/recommended";
                    var result = httpClient.GetAsync(request, HttpCompletionOption.ResponseContentRead).Result;
                    var json = JObject.Parse(result.Content.ReadAsStringAsync().Result);
                    var fastestSatoshiPerByteFee = json.Value<decimal>("fastestFee");
                    fee = new Money(fastestSatoshiPerByteFee * txSizeInBytes, MoneyUnit.Satoshi);
                }
                catch
                {
                    throw new Exception("Can't get tx fee");
                }
                //WriteLine($"Fee: {fee.ToDecimal(MoneyUnit.BTC).ToString("0.#############################")}btc");

                // 5. How much money we can spend?
                Money availableAmount = unspentCoins.Sum(x => x.Key.Amount);

                // 6. How much to spend?
                Money amountToSend = null;
                string amountString = txtSendBitcoinAmount.Text;
                if (string.Equals(amountString, "all", StringComparison.OrdinalIgnoreCase))
                {
                    amountToSend = availableAmount;
                    amountToSend -= fee;
                }
                else
                {
                    amountToSend = ParseBtcString(amountString);
                }

                // 7. Do some checks
                if (amountToSend < Money.Zero || availableAmount < amountToSend + fee)
                    throw new Exception("Not enough coins.");

                decimal feePc = Math.Round((100 * fee.ToDecimal(MoneyUnit.BTC)) / amountToSend.ToDecimal(MoneyUnit.BTC));
                if (feePc > 1)
                {
                    //WriteLine();
                    //WriteLine($"The transaction fee is {feePc.ToString("0.#")}% of your transaction amount.");
                    //WriteLine($"Sending:\t {amountToSend.ToDecimal(MoneyUnit.BTC).ToString("0.#############################")}btc");
                    //WriteLine($"Fee:\t\t {fee.ToDecimal(MoneyUnit.BTC).ToString("0.#############################")}btc");
                    //ConsoleKey response = GetYesNoAnswerFromUser();
                }

                var totalOutAmount = amountToSend + fee;

                // 8. Select coins
                //WriteLine("Selecting coins...");
                var coinsToSpend = new HashSet<Coin>();
                var unspentConfirmedCoins = new List<Coin>();
                var unspentUnconfirmedCoins = new List<Coin>();
                foreach (var elem in unspentCoins)
                    if (elem.Value) unspentConfirmedCoins.Add(elem.Key);
                    else unspentUnconfirmedCoins.Add(elem.Key);

                bool haveEnough = SelectCoins(ref coinsToSpend, totalOutAmount, unspentConfirmedCoins);
                if (!haveEnough)
                    haveEnough = SelectCoins(ref coinsToSpend, totalOutAmount, unspentUnconfirmedCoins);
                if (!haveEnough)
                    throw new Exception("Not enough funds.");

                // 9. Get signing keys
                var signingKeys = new HashSet<ISecret>();
                foreach (var coin in coinsToSpend)
                {
                    foreach (var elem in operationsPerNotEmptyPrivateKeys)
                    {
                        if (elem.Key.ScriptPubKey == coin.ScriptPubKey)
                            signingKeys.Add(elem.Key);
                    }
                }

                // 10. Build the transaction
                // WriteLine("Signing transaction...");
                TransactionBuilder builder = null;
                var tx = builder
                    .AddCoins(coinsToSpend)
                    .AddKeys(signingKeys.ToArray())
                    .Send(addressToSend, amountToSend)
                    .SetChange(changeScriptPubKey)
                    .SendFees(fee)
                    .BuildTransaction(true);

                if (!builder.Verify(tx))
                    throw new Exception("Couldn't build the transaction.");

                //WriteLine($"Transaction Id: {tx.GetHash()}");

                var qBitClient = new QBitNinjaClient(Config.Network);

                // QBit's success response is buggy so let's check manually, too		
                BroadcastResponse broadcastResponse;
                var success = false;
                var tried = 0;
                var maxTry = 7;
                do
                {
                    tried++;
                    //WriteLine($"Try broadcasting transaction... ({tried})");
                    broadcastResponse = qBitClient.Broadcast(tx).Result;
                    var getTxResp = qBitClient.GetTransaction(tx.GetHash()).Result;
                    if (getTxResp == null)
                    {
                        Thread.Sleep(3000);
                        continue;
                    }
                    else
                    {
                        success = true;
                        break;
                    }
                } while (tried <= maxTry);
                if (!success)
                {
                    if (broadcastResponse.Error != null)
                    {
                        //WriteLine($"Error code: {broadcastResponse.Error.ErrorCode} Reason: {broadcastResponse.Error.Reason}");
                    }
                    throw new Exception($"The transaction might not have been successfully broadcasted. Please check the Transaction ID in a block explorer.");
                }
                throw new Exception("Transaction is successfully propagated on the network.");
            }
            else if (Config.ConnectionType == ConnectionType.FullNode)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new Exception("Invalid connection type.");
            }
        }
    }
}
