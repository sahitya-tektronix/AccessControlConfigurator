namespace AccessControlConfigurator.Forms
{
    partial class ResetPasswordForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblUsername = new System.Windows.Forms.Label();
            txtUsername = new System.Windows.Forms.TextBox();
            lblNewPassword = new System.Windows.Forms.Label();
            txtNewPassword = new System.Windows.Forms.TextBox();
            lblConfirmPassword = new System.Windows.Forms.Label();
            txtConfirmPassword = new System.Windows.Forms.TextBox();
            btnReset = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            SuspendLayout();

            lblUsername.AutoSize = true;
            lblUsername.Location = new System.Drawing.Point(20, 20);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new System.Drawing.Size(63, 15);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username:";

            txtUsername.Location = new System.Drawing.Point(20, 38);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new System.Drawing.Size(260, 23);
            txtUsername.TabIndex = 1;

            lblNewPassword.AutoSize = true;
            lblNewPassword.Location = new System.Drawing.Point(20, 70);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new System.Drawing.Size(95, 15);
            lblNewPassword.TabIndex = 2;
            lblNewPassword.Text = "New Password:";

            txtNewPassword.Location = new System.Drawing.Point(20, 88);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.Size = new System.Drawing.Size(260, 23);
            txtNewPassword.TabIndex = 3;

            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new System.Drawing.Point(20, 120);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new System.Drawing.Size(113, 15);
            lblConfirmPassword.TabIndex = 4;
            lblConfirmPassword.Text = "Confirm Password:";

            txtConfirmPassword.Location = new System.Drawing.Point(20, 138);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Size = new System.Drawing.Size(260, 23);
            txtConfirmPassword.TabIndex = 5;

            btnReset.Location = new System.Drawing.Point(120, 180);
            btnReset.Name = "btnReset";
            btnReset.Size = new System.Drawing.Size(80, 30);
            btnReset.TabIndex = 6;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += new System.EventHandler(btnReset_Click);

            btnCancel.Location = new System.Drawing.Point(210, 180);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(80, 30);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new System.EventHandler(btnCancel_Click);

            ClientSize = new System.Drawing.Size(300, 220);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblNewPassword);
            Controls.Add(txtNewPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(btnReset);
            Controls.Add(btnCancel);

            Name = "ResetPasswordForm";
            Text = "Reset Password";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Load += new System.EventHandler(ResetPasswordForm_Load);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
