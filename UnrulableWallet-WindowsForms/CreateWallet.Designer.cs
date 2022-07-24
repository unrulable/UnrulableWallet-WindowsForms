namespace UnrulableWallet.UI
{
    partial class CreateWallet
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
            this.lblNewWalletName = new System.Windows.Forms.Label();
            this.lblNewWalletPassword = new System.Windows.Forms.Label();
            this.txtNewWalletName = new System.Windows.Forms.TextBox();
            this.txtNewWalletPassword = new System.Windows.Forms.TextBox();
            this.txtNewWalletConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblNewWalletConfirmPassword = new System.Windows.Forms.Label();
            this.btnCreateNewWallet = new System.Windows.Forms.Button();
            this.groupBoxNewWalletDetails = new System.Windows.Forms.GroupBox();
            this.lblWalletCreateStatusValue = new System.Windows.Forms.Label();
            this.lblWalletCreateStatus = new System.Windows.Forms.Label();
            this.lblYouCanRecoverYourWalletInfoMessage = new System.Windows.Forms.Label();
            this.lblWriteDownTheMnemonic = new System.Windows.Forms.Label();
            this.lblWalletFilePathValue = new System.Windows.Forms.TextBox();
            this.lblWalletFilePath = new System.Windows.Forms.Label();
            this.lblMnemonicValue = new System.Windows.Forms.TextBox();
            this.lblMnemonic = new System.Windows.Forms.Label();
            this.groupBoxNewWalletDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNewWalletName
            // 
            this.lblNewWalletName.AutoSize = true;
            this.lblNewWalletName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewWalletName.Location = new System.Drawing.Point(56, 54);
            this.lblNewWalletName.Name = "lblNewWalletName";
            this.lblNewWalletName.Size = new System.Drawing.Size(204, 25);
            this.lblNewWalletName.TabIndex = 0;
            this.lblNewWalletName.Text = "New Wallet Name:";
            // 
            // lblNewWalletPassword
            // 
            this.lblNewWalletPassword.AutoSize = true;
            this.lblNewWalletPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewWalletPassword.Location = new System.Drawing.Point(56, 122);
            this.lblNewWalletPassword.Name = "lblNewWalletPassword";
            this.lblNewWalletPassword.Size = new System.Drawing.Size(121, 25);
            this.lblNewWalletPassword.TabIndex = 1;
            this.lblNewWalletPassword.Text = "Password:";
            // 
            // txtNewWalletName
            // 
            this.txtNewWalletName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewWalletName.Location = new System.Drawing.Point(281, 48);
            this.txtNewWalletName.Name = "txtNewWalletName";
            this.txtNewWalletName.Size = new System.Drawing.Size(633, 31);
            this.txtNewWalletName.TabIndex = 2;
            // 
            // txtNewWalletPassword
            // 
            this.txtNewWalletPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewWalletPassword.Location = new System.Drawing.Point(281, 116);
            this.txtNewWalletPassword.Name = "txtNewWalletPassword";
            this.txtNewWalletPassword.PasswordChar = '*';
            this.txtNewWalletPassword.Size = new System.Drawing.Size(633, 31);
            this.txtNewWalletPassword.TabIndex = 3;
            this.txtNewWalletPassword.UseSystemPasswordChar = true;
            // 
            // txtNewWalletConfirmPassword
            // 
            this.txtNewWalletConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewWalletConfirmPassword.Location = new System.Drawing.Point(281, 176);
            this.txtNewWalletConfirmPassword.Name = "txtNewWalletConfirmPassword";
            this.txtNewWalletConfirmPassword.PasswordChar = '*';
            this.txtNewWalletConfirmPassword.Size = new System.Drawing.Size(633, 31);
            this.txtNewWalletConfirmPassword.TabIndex = 4;
            this.txtNewWalletConfirmPassword.UseSystemPasswordChar = true;
            // 
            // lblNewWalletConfirmPassword
            // 
            this.lblNewWalletConfirmPassword.AutoSize = true;
            this.lblNewWalletConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewWalletConfirmPassword.Location = new System.Drawing.Point(56, 182);
            this.lblNewWalletConfirmPassword.Name = "lblNewWalletConfirmPassword";
            this.lblNewWalletConfirmPassword.Size = new System.Drawing.Size(209, 25);
            this.lblNewWalletConfirmPassword.TabIndex = 5;
            this.lblNewWalletConfirmPassword.Text = "Confirm Password:";
            // 
            // btnCreateNewWallet
            // 
            this.btnCreateNewWallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateNewWallet.Location = new System.Drawing.Point(281, 241);
            this.btnCreateNewWallet.Name = "btnCreateNewWallet";
            this.btnCreateNewWallet.Size = new System.Drawing.Size(633, 46);
            this.btnCreateNewWallet.TabIndex = 6;
            this.btnCreateNewWallet.Text = "Create";
            this.btnCreateNewWallet.UseVisualStyleBackColor = true;
            this.btnCreateNewWallet.Click += new System.EventHandler(this.btnCreateNewWallet_Click);
            // 
            // groupBoxNewWalletDetails
            // 
            this.groupBoxNewWalletDetails.Controls.Add(this.lblWalletCreateStatusValue);
            this.groupBoxNewWalletDetails.Controls.Add(this.lblWalletCreateStatus);
            this.groupBoxNewWalletDetails.Controls.Add(this.lblYouCanRecoverYourWalletInfoMessage);
            this.groupBoxNewWalletDetails.Controls.Add(this.lblWriteDownTheMnemonic);
            this.groupBoxNewWalletDetails.Controls.Add(this.lblWalletFilePathValue);
            this.groupBoxNewWalletDetails.Controls.Add(this.lblWalletFilePath);
            this.groupBoxNewWalletDetails.Controls.Add(this.lblMnemonicValue);
            this.groupBoxNewWalletDetails.Controls.Add(this.lblMnemonic);
            this.groupBoxNewWalletDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxNewWalletDetails.Location = new System.Drawing.Point(61, 349);
            this.groupBoxNewWalletDetails.Name = "groupBoxNewWalletDetails";
            this.groupBoxNewWalletDetails.Size = new System.Drawing.Size(931, 388);
            this.groupBoxNewWalletDetails.TabIndex = 7;
            this.groupBoxNewWalletDetails.TabStop = false;
            this.groupBoxNewWalletDetails.Text = "New Wallet Details";
            this.groupBoxNewWalletDetails.Visible = false;
            // 
            // lblWalletCreateStatusValue
            // 
            this.lblWalletCreateStatusValue.AutoSize = true;
            this.lblWalletCreateStatusValue.Location = new System.Drawing.Point(114, 37);
            this.lblWalletCreateStatusValue.Name = "lblWalletCreateStatusValue";
            this.lblWalletCreateStatusValue.Size = new System.Drawing.Size(0, 25);
            this.lblWalletCreateStatusValue.TabIndex = 7;
            // 
            // lblWalletCreateStatus
            // 
            this.lblWalletCreateStatus.AutoSize = true;
            this.lblWalletCreateStatus.Location = new System.Drawing.Point(21, 38);
            this.lblWalletCreateStatus.Name = "lblWalletCreateStatus";
            this.lblWalletCreateStatus.Size = new System.Drawing.Size(86, 25);
            this.lblWalletCreateStatus.TabIndex = 6;
            this.lblWalletCreateStatus.Text = "Status:";
            // 
            // lblYouCanRecoverYourWalletInfoMessage
            // 
            this.lblYouCanRecoverYourWalletInfoMessage.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblYouCanRecoverYourWalletInfoMessage.Location = new System.Drawing.Point(158, 214);
            this.lblYouCanRecoverYourWalletInfoMessage.Name = "lblYouCanRecoverYourWalletInfoMessage";
            this.lblYouCanRecoverYourWalletInfoMessage.Size = new System.Drawing.Size(695, 82);
            this.lblYouCanRecoverYourWalletInfoMessage.TabIndex = 5;
            this.lblYouCanRecoverYourWalletInfoMessage.Text = "With the mnemonic words AND your password you can recover this wallet by using th" +
    "e recover-wallet command.";
            // 
            // lblWriteDownTheMnemonic
            // 
            this.lblWriteDownTheMnemonic.AutoSize = true;
            this.lblWriteDownTheMnemonic.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblWriteDownTheMnemonic.Location = new System.Drawing.Point(21, 82);
            this.lblWriteDownTheMnemonic.Name = "lblWriteDownTheMnemonic";
            this.lblWriteDownTheMnemonic.Size = new System.Drawing.Size(458, 25);
            this.lblWriteDownTheMnemonic.TabIndex = 4;
            this.lblWriteDownTheMnemonic.Text = "Write down the following mnemonic words.";
            // 
            // lblWalletFilePathValue
            // 
            this.lblWalletFilePathValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWalletFilePathValue.Location = new System.Drawing.Point(163, 319);
            this.lblWalletFilePathValue.Name = "lblWalletFilePathValue";
            this.lblWalletFilePathValue.Size = new System.Drawing.Size(690, 31);
            this.lblWalletFilePathValue.TabIndex = 3;
            // 
            // lblWalletFilePath
            // 
            this.lblWalletFilePath.AutoSize = true;
            this.lblWalletFilePath.Location = new System.Drawing.Point(21, 319);
            this.lblWalletFilePath.Name = "lblWalletFilePath";
            this.lblWalletFilePath.Size = new System.Drawing.Size(113, 25);
            this.lblWalletFilePath.TabIndex = 2;
            this.lblWalletFilePath.Text = "File Path:";
            // 
            // lblMnemonicValue
            // 
            this.lblMnemonicValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMnemonicValue.Location = new System.Drawing.Point(163, 121);
            this.lblMnemonicValue.Multiline = true;
            this.lblMnemonicValue.Name = "lblMnemonicValue";
            this.lblMnemonicValue.Size = new System.Drawing.Size(690, 80);
            this.lblMnemonicValue.TabIndex = 1;
            // 
            // lblMnemonic
            // 
            this.lblMnemonic.AutoSize = true;
            this.lblMnemonic.Location = new System.Drawing.Point(21, 121);
            this.lblMnemonic.Name = "lblMnemonic";
            this.lblMnemonic.Size = new System.Drawing.Size(126, 25);
            this.lblMnemonic.TabIndex = 0;
            this.lblMnemonic.Text = "Mnemonic:";
            // 
            // CreateWallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 768);
            this.Controls.Add(this.groupBoxNewWalletDetails);
            this.Controls.Add(this.btnCreateNewWallet);
            this.Controls.Add(this.lblNewWalletConfirmPassword);
            this.Controls.Add(this.txtNewWalletConfirmPassword);
            this.Controls.Add(this.txtNewWalletPassword);
            this.Controls.Add(this.txtNewWalletName);
            this.Controls.Add(this.lblNewWalletPassword);
            this.Controls.Add(this.lblNewWalletName);
            this.Name = "CreateWallet";
            this.Text = "CreateWallet";
            this.groupBoxNewWalletDetails.ResumeLayout(false);
            this.groupBoxNewWalletDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNewWalletName;
        private System.Windows.Forms.Label lblNewWalletPassword;
        private System.Windows.Forms.TextBox txtNewWalletName;
        private System.Windows.Forms.TextBox txtNewWalletPassword;
        private System.Windows.Forms.TextBox txtNewWalletConfirmPassword;
        private System.Windows.Forms.Label lblNewWalletConfirmPassword;
        private System.Windows.Forms.Button btnCreateNewWallet;
        private System.Windows.Forms.GroupBox groupBoxNewWalletDetails;
        private System.Windows.Forms.Label lblWalletCreateStatusValue;
        private System.Windows.Forms.Label lblWalletCreateStatus;
        private System.Windows.Forms.Label lblYouCanRecoverYourWalletInfoMessage;
        private System.Windows.Forms.Label lblWriteDownTheMnemonic;
        private System.Windows.Forms.TextBox lblWalletFilePathValue;
        private System.Windows.Forms.Label lblWalletFilePath;
        private System.Windows.Forms.TextBox lblMnemonicValue;
        private System.Windows.Forms.Label lblMnemonic;
    }
}