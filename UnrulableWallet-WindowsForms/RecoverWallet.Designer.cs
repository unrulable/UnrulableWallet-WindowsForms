namespace UnrulableWallet.UI
{
    partial class RecoverWallet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPleaseProvideMnemonic = new System.Windows.Forms.Label();
            this.lblProvideYourPassword = new System.Windows.Forms.Label();
            this.btnRecoverWallet = new System.Windows.Forms.Button();
            this.txtProvidePasswordRecoverWallet = new System.Windows.Forms.TextBox();
            this.txtProvideYourMnemonicRecoverWallet = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblPleaseProvideMnemonic
            // 
            this.lblPleaseProvideMnemonic.Location = new System.Drawing.Point(40, 40);
            this.lblPleaseProvideMnemonic.Name = "lblPleaseProvideMnemonic";
            this.lblPleaseProvideMnemonic.Size = new System.Drawing.Size(548, 62);
            this.lblPleaseProvideMnemonic.TabIndex = 9;
            this.lblPleaseProvideMnemonic.Text = "Provide your mnemonic words, separated by spaces:";
            // 
            // lblProvideYourPassword
            // 
            this.lblProvideYourPassword.Location = new System.Drawing.Point(40, 285);
            this.lblProvideYourPassword.Name = "lblProvideYourPassword";
            this.lblProvideYourPassword.Size = new System.Drawing.Size(873, 85);
            this.lblProvideYourPassword.TabIndex = 8;
            this.lblProvideYourPassword.Text = "Provide your password. Please note the wallet cannot check if your password is co" +
    "rrect or not. If you provide a wrong password a wallet will be recovered with yo" +
    "ur provided mnemonic AND password pair:";
            // 
            // btnRecoverWallet
            // 
            this.btnRecoverWallet.Location = new System.Drawing.Point(45, 437);
            this.btnRecoverWallet.Name = "btnRecoverWallet";
            this.btnRecoverWallet.Size = new System.Drawing.Size(868, 39);
            this.btnRecoverWallet.TabIndex = 7;
            this.btnRecoverWallet.Text = "Recover";
            this.btnRecoverWallet.UseVisualStyleBackColor = true;
            this.btnRecoverWallet.Click += new System.EventHandler(this.btnRecoverWallet_Click);
            // 
            // txtProvidePasswordRecoverWallet
            // 
            this.txtProvidePasswordRecoverWallet.Location = new System.Drawing.Point(45, 390);
            this.txtProvidePasswordRecoverWallet.Name = "txtProvidePasswordRecoverWallet";
            this.txtProvidePasswordRecoverWallet.PasswordChar = '*';
            this.txtProvidePasswordRecoverWallet.Size = new System.Drawing.Size(868, 31);
            this.txtProvidePasswordRecoverWallet.TabIndex = 6;
            this.txtProvidePasswordRecoverWallet.UseSystemPasswordChar = true;
            // 
            // txtProvideYourMnemonicRecoverWallet
            // 
            this.txtProvideYourMnemonicRecoverWallet.Location = new System.Drawing.Point(45, 105);
            this.txtProvideYourMnemonicRecoverWallet.Multiline = true;
            this.txtProvideYourMnemonicRecoverWallet.Name = "txtProvideYourMnemonicRecoverWallet";
            this.txtProvideYourMnemonicRecoverWallet.Size = new System.Drawing.Size(868, 136);
            this.txtProvideYourMnemonicRecoverWallet.TabIndex = 5;
            // 
            // RecoverWallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 679);
            this.Controls.Add(this.lblPleaseProvideMnemonic);
            this.Controls.Add(this.lblProvideYourPassword);
            this.Controls.Add(this.btnRecoverWallet);
            this.Controls.Add(this.txtProvidePasswordRecoverWallet);
            this.Controls.Add(this.txtProvideYourMnemonicRecoverWallet);
            this.Name = "RecoverWallet";
            this.Text = "RecoverWallet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPleaseProvideMnemonic;
        private System.Windows.Forms.Label lblProvideYourPassword;
        private System.Windows.Forms.Button btnRecoverWallet;
        private System.Windows.Forms.TextBox txtProvidePasswordRecoverWallet;
        private System.Windows.Forms.TextBox txtProvideYourMnemonicRecoverWallet;
    }
}