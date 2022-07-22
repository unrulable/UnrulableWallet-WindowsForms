﻿using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static UnrulableWallet.UI.Shared.Enums;
using NBitcoin;
using System.IO;
using System.Linq;
using static System.Console;
using HBitcoin.KeyManagement;
using static UnrulableWallet.UI.Wrapper.QBitNinjaWrapper;
using static UnrulableWallet.UI.Shared.Helpers;

namespace UnrulableWallet.UI
{
    public partial class Main : Form
    {
        public static int currentAvailableWalletsCount;

        #region Models

        public int CurrentAvailableWalletsCount => this.comboBoxYourAvailableWallets.Items.Count;
        public string CurrentOpenedWalletName => this.comboBoxYourAvailableWallets.SelectedItem.ToString();

        #endregion

        #region Constructors
        public Main()
        {
            InitializeComponent();
            groupBoxNewWalletDetails.Hide();
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

        private void GenerateNewWalletCreatedDisplay(Mnemonic mnemonic, string walletFilePath)
        {
            groupBoxNewWalletDetails.Show();
            lblMnemonicValue.Text = mnemonic.ToString();
            lblWalletFilePathValue.Text = walletFilePath;
            lblWalletCreateStatusValue.Text = "Successful";
            lblWalletCreateStatusValue.ForeColor = Color.Green;
        }

        private void GenerateWalletRecoveredDisplay(Mnemonic mnemonic, string walletFilePath)
        {
            groupBoxNewWalletDetails.Show();
            lblMnemonicValue.Text = mnemonic.ToString();
            lblWalletFilePathValue.Text = walletFilePath;
            lblWalletCreateStatusValue.Text = "Successful";
            lblWalletCreateStatusValue.ForeColor = Color.Green;
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

        private void btnRecoverWallet_Click(object sender, EventArgs e)
        {
            var walletFilePath = GetWalletFilePath("");
            AssertWalletNotExists(walletFilePath);

            var mnemonicString = txtProvideYourMnemonicRecoverWallet.Text;
            AssertCorrectMnemonicFormat(mnemonicString);
            var mnemonic = new Mnemonic(mnemonicString);
            var password = txtProvidePasswordRecoverWallet.Text;

            Safe safe = Safe.Recover(mnemonic, password, walletFilePath, Config.Network);
            // If no exception thrown the wallet is successfully recovered.
            GenerateWalletRecoveredDisplay(mnemonic, walletFilePath);
        }

        private void btnOpenWallet_Click(object sender, EventArgs e)
        {
            lblStatusRetrievingWalletDetails.Show();
            var walletFilePath = GetWalletFilePath(CurrentOpenedWalletName);
            Safe safe = DecryptWalletByAskingForPassword(walletFilePath, txtOpenWalletPassword.Text);
            List<string> confirmBalances = ShowBalances(walletFilePath, safe);

            groupBoxCurrentWalletOpenDetails.Show();
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

        private void toolStripMenuItemOpenWallet_Click(object sender, EventArgs e)
        {
            groupBoxCurrentWalletOpenDetails.Hide();
            groupBoxEntryView.Show();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Console.WriteLine("SEND!!!!");
            groupBoxSendBitcoin.Show();
            //groupBoxWalletTransactions.Hide();
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Transactions!!!!");
            groupBoxWalletTransactions.Show();
            groupBoxSendBitcoin.Hide();
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            var walletFilePath = GetWalletFilePath(CurrentOpenedWalletName);
            Safe safe = DecryptWalletByAskingForPassword(walletFilePath, txtOpenWalletPassword.Text);

            if (Config.ConnectionType == ConnectionType.Http)
            {
                Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerReceiveAddresses = QueryOperationsPerSafeAddresses(safe, 7, HdPathType.Receive);

                Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                pictureBoxBitcoinReceiveAddressQrCode.Image = qrcode.Draw(operationsPerReceiveAddresses.ElementAt(0).Key.ToString(), 50);
                //foreach (var elem in operationsPerReceiveAddresses)
                //    if (elem.Value.Count == 0)
                //        WriteLine($"{elem.Key}");
            }
            else if (Config.ConnectionType == ConnectionType.FullNode)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new Exception("Invalid connection type.");
            }

            groupBoxReceiveBitcoin.Show();
        }

        #endregion

        #region Private Methods
        private string CalculateBalanceTotal(List<string> confirmBalances)
        {
            return "0";
        }

        private static List<string> GetAvailableBitcoinWallets()
        {
            string[] wallets = Directory.GetFiles(@"Wallets", "*");
            List<string> walletsList = new List<string>();

            // The stored wallet count != wallets we just pulled from directory
            // then update the list so we have the full list of available wallets
            if (currentAvailableWalletsCount != wallets.Length)
            {
                foreach (string wallet in wallets)
                {
                    string fileName = Path.GetFileName(wallet).Replace(".json", string.Empty);
                    walletsList.Add(fileName);
                    currentAvailableWalletsCount++;
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
