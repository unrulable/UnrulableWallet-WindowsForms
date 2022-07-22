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
            this.SuspendLayout();
            // 
            // lblNewWalletName
            // 
            this.lblNewWalletName.AutoSize = true;
            this.lblNewWalletName.Location = new System.Drawing.Point(56, 54);
            this.lblNewWalletName.Name = "lblNewWalletName";
            this.lblNewWalletName.Size = new System.Drawing.Size(188, 25);
            this.lblNewWalletName.TabIndex = 0;
            this.lblNewWalletName.Text = "New Wallet Name:";
            // 
            // lblNewWalletPassword
            // 
            this.lblNewWalletPassword.AutoSize = true;
            this.lblNewWalletPassword.Location = new System.Drawing.Point(56, 122);
            this.lblNewWalletPassword.Name = "lblNewWalletPassword";
            this.lblNewWalletPassword.Size = new System.Drawing.Size(112, 25);
            this.lblNewWalletPassword.TabIndex = 1;
            this.lblNewWalletPassword.Text = "Password:";
            // 
            // txtNewWalletName
            // 
            this.txtNewWalletName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewWalletName.Location = new System.Drawing.Point(250, 48);
            this.txtNewWalletName.Name = "txtNewWalletName";
            this.txtNewWalletName.Size = new System.Drawing.Size(268, 31);
            this.txtNewWalletName.TabIndex = 2;
            // 
            // txtNewWalletPassword
            // 
            this.txtNewWalletPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewWalletPassword.Location = new System.Drawing.Point(250, 116);
            this.txtNewWalletPassword.Name = "txtNewWalletPassword";
            this.txtNewWalletPassword.PasswordChar = '*';
            this.txtNewWalletPassword.Size = new System.Drawing.Size(268, 31);
            this.txtNewWalletPassword.TabIndex = 3;
            this.txtNewWalletPassword.UseSystemPasswordChar = true;
            // 
            // txtNewWalletConfirmPassword
            // 
            this.txtNewWalletConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewWalletConfirmPassword.Location = new System.Drawing.Point(250, 176);
            this.txtNewWalletConfirmPassword.Name = "txtNewWalletConfirmPassword";
            this.txtNewWalletConfirmPassword.PasswordChar = '*';
            this.txtNewWalletConfirmPassword.Size = new System.Drawing.Size(268, 31);
            this.txtNewWalletConfirmPassword.TabIndex = 4;
            this.txtNewWalletConfirmPassword.UseSystemPasswordChar = true;
            // 
            // lblNewWalletConfirmPassword
            // 
            this.lblNewWalletConfirmPassword.AutoSize = true;
            this.lblNewWalletConfirmPassword.Location = new System.Drawing.Point(56, 182);
            this.lblNewWalletConfirmPassword.Name = "lblNewWalletConfirmPassword";
            this.lblNewWalletConfirmPassword.Size = new System.Drawing.Size(192, 25);
            this.lblNewWalletConfirmPassword.TabIndex = 5;
            this.lblNewWalletConfirmPassword.Text = "Confirm Password:";
            // 
            // btnCreateNewWallet
            // 
            this.btnCreateNewWallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateNewWallet.Location = new System.Drawing.Point(250, 241);
            this.btnCreateNewWallet.Name = "btnCreateNewWallet";
            this.btnCreateNewWallet.Size = new System.Drawing.Size(268, 46);
            this.btnCreateNewWallet.TabIndex = 6;
            this.btnCreateNewWallet.Text = "Create";
            this.btnCreateNewWallet.UseVisualStyleBackColor = true;
            this.btnCreateNewWallet.Click += new System.EventHandler(this.btnCreateNewWallet_Click);
            // 
            // CreateWallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 355);
            this.Controls.Add(this.btnCreateNewWallet);
            this.Controls.Add(this.lblNewWalletConfirmPassword);
            this.Controls.Add(this.txtNewWalletConfirmPassword);
            this.Controls.Add(this.txtNewWalletPassword);
            this.Controls.Add(this.txtNewWalletName);
            this.Controls.Add(this.lblNewWalletPassword);
            this.Controls.Add(this.lblNewWalletName);
            this.Name = "CreateWallet";
            this.Text = "CreateWallet";
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
    }
}