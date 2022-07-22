using System;
using System.Windows.Forms;
using HBitcoin.KeyManagement;
using NBitcoin;
using static UnrulableWallet.UI.Shared.Helpers;

namespace UnrulableWallet.UI
{
    public partial class CreateWallet : Form
    {
        public CreateWallet()
        {
            InitializeComponent();
        }

        private void btnCreateNewWallet_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                var walletFilePath = GetWalletFilePath(txtNewWalletName.Text);
                AssertWalletNotExists(walletFilePath);

                // Create wallet
                string pw = txtNewWalletPassword.Text;
                Mnemonic mnemonic;
                Safe safe = Safe.Create(out mnemonic, pw, walletFilePath, Config.Network);

                // Close the form
                Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
