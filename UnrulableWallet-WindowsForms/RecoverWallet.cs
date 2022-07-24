using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HBitcoin.KeyManagement;
using NBitcoin;
using UnrulableWallet.UI.Shared;

namespace UnrulableWallet.UI
{
    public partial class RecoverWallet : Form
    {
        public RecoverWallet()
        {
            InitializeComponent();
        }

        private void btnRecoverWallet_Click(object sender, EventArgs e)
        {
            var walletFilePath = Helpers.GetWalletFilePath("");
            Helpers.AssertWalletNotExists(walletFilePath);

            var mnemonicString = txtProvideYourMnemonicRecoverWallet.Text;
            Helpers.AssertCorrectMnemonicFormat(mnemonicString);
            var mnemonic = new Mnemonic(mnemonicString);
            var password = txtProvidePasswordRecoverWallet.Text;

            Safe safe = Safe.Recover(mnemonic, password, walletFilePath, Config.Network);
            // If no exception thrown the wallet is successfully recovered.
            //GenerateWalletRecoveredDisplay(mnemonic, walletFilePath);
        }
    }
}
