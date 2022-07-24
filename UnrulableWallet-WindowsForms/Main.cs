using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static UnrulableWallet.UI.Shared.Enums;
using NBitcoin;
using System.IO;
using System.Linq;
using HBitcoin.KeyManagement;
using UnrulableWallet.UI.Models;
using static UnrulableWallet.UI.Wrapper.QBitNinjaWrapper;
using static UnrulableWallet.UI.Shared.Helpers;

namespace UnrulableWallet.UI
{
    public partial class Main : Form
    {
        #region Fields

        private ReceiveBitcoinModel _receiveBitcoinModel;

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
            // It also creates it with default settings if doesn't exist
            Config.Load();

            // Load available wallets to open
            PopulateAvailableWalletsDropDown(GetAvailableBitcoinWallets());
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
            lblTransactionStatusValue.Text = $"{transaction.Block.Confirmations} Confirmations";
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

        private void btnCreateWallet_Click(object sender, EventArgs e)
        {
        }

        private void btnOpenWallet_Click(object sender, EventArgs e)
        {
            lblStatusRetrievingWalletDetails.Show();
            var walletFilePath = GetWalletFilePath(CurrentOpenedWalletName);
            Safe safe = DecryptWalletByAskingForPassword(walletFilePath, txtOpenWalletPassword.Text);
            List<string> confirmBalances = ShowBalances(walletFilePath, safe);

            tabControlaUnrulableWallet.Show();
            groupBoxEntryView.Hide();
            lblCurrentWalletNameOpenValue.Text = CurrentOpenedWalletName;
            lblOpenWalletBalanceValue.Text = confirmBalances.Count == 0 ? "0" : CalculateBalanceTotal(confirmBalances);
            lblTransactionCountValue.Text = confirmBalances.Count.ToString();
        }

        private void toolStripMenuItemWalletCreate_Click(object sender, EventArgs e)
        {
            CreateWallet createWalletForm = new CreateWallet();
            createWalletForm.ShowDialog();

            //Refresh available wallets to open list
            PopulateAvailableWalletsDropDown(GetAvailableBitcoinWallets());
        }

        private void toolStripMenuItemRecoverWallet_Click(object sender, EventArgs e)
        {
            RecoverWallet recoverWalletForm = new RecoverWallet();
            recoverWalletForm.ShowDialog();
        }

        private void toolStripMenuItemOpenWallet_Click(object sender, EventArgs e)
        {
            groupBoxCurrentWalletOpenDetails.Hide();
            groupBoxEntryView.Show();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            groupBoxSendBitcoin.Show();
            groupBoxWalletTransactions.Hide();
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            groupBoxWalletTransactions.Show();
            groupBoxSendBitcoin.Hide();
        }

        private void listViewAvailableBitcoinAddress_SelectedValueChanged(object sender, EventArgs e)
        {
            // Get the currently selected item in the ListBox.
            if (listBoxAllAvailableBitcoinAddresses?.SelectedItem != null)
            {
                string currentSelectedBitcoinAddressFromList = listBoxAllAvailableBitcoinAddresses.SelectedItem.ToString();
                _receiveBitcoinModel.BitcoinAddress = currentSelectedBitcoinAddressFromList;
                txtCurrentBitcoinReceiveAddress.Text = currentSelectedBitcoinAddressFromList;
                receiveBitcoinModelBindingSource.DataSource = _receiveBitcoinModel;
                GenerateQrCodeDisplay(currentSelectedBitcoinAddressFromList, 25, pictureBoxBitcoinReceiveAddressQrCode);
            }
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
                    MessageBox.Show("hello world");
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

        private List<string> GetAvailableBitcoinWallets()
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

            return walletsList;
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
    }
}
