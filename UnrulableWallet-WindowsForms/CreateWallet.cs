using System;
using System.Drawing;
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

                groupBoxNewWalletDetails.Show();
                lblMnemonicValue.Text = mnemonic.ToString();
                lblWalletFilePathValue.Text = walletFilePath;
                lblWalletCreateStatusValue.Text = "Successful";
                lblWalletCreateStatusValue.ForeColor = Color.Green;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
