namespace AccessControlConfigurator
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlMain = new Panel();
            pnlBanner = new Panel();
            picBanner = new PictureBox();
            pnlLogin = new Panel();
            lblTitle = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lnkForgot = new LinkLabel();
            btnLogin = new Button();
            btnExit = new Button();
            pnlMain.SuspendLayout();
            pnlBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBanner).BeginInit();
            pnlLogin.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(pnlBanner);
            pnlMain.Controls.Add(pnlLogin);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1100, 600);
            pnlMain.TabIndex = 0;
            // 
            // pnlBanner
            // 
            pnlBanner.BackColor = Color.FromArgb(24, 129, 170);
            pnlBanner.Controls.Add(picBanner);
            pnlBanner.Dock = DockStyle.Fill;
            pnlBanner.Location = new Point(0, 0);
            pnlBanner.Name = "pnlBanner";
            pnlBanner.Size = new Size(740, 600);
            pnlBanner.TabIndex = 0;
            // 
            // picBanner
            // 
            picBanner.BackColor = Color.FromArgb(0, 0, 64);
            picBanner.Dock = DockStyle.Fill;
            picBanner.Image = Properties.Resources.hid_banner;
            picBanner.Location = new Point(0, 0);
            picBanner.Name = "picBanner";
            picBanner.Size = new Size(740, 600);
            picBanner.SizeMode = PictureBoxSizeMode.Zoom;
            picBanner.TabIndex = 1;
            picBanner.TabStop = false;
            // 
            // pnlLogin
            // 
            pnlLogin.BackColor = Color.White;
            pnlLogin.Controls.Add(lblTitle);
            pnlLogin.Controls.Add(lblUsername);
            pnlLogin.Controls.Add(txtUsername);
            pnlLogin.Controls.Add(lblPassword);
            pnlLogin.Controls.Add(txtPassword);
            pnlLogin.Controls.Add(lnkForgot);
            pnlLogin.Controls.Add(btnLogin);
            pnlLogin.Controls.Add(btnExit);
            pnlLogin.Dock = DockStyle.Right;
            pnlLogin.Location = new Point(740, 0);
            pnlLogin.Name = "pnlLogin";
            pnlLogin.Size = new Size(360, 600);
            pnlLogin.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 102, 204);
            lblTitle.Location = new Point(120, 60);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(117, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Sign In";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(40, 140);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(75, 20);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(40, 160);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(280, 27);
            txtUsername.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(40, 200);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(70, 20);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(40, 220);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(280, 27);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lnkForgot
            // 
            lnkForgot.AutoSize = true;
            lnkForgot.Location = new Point(195, 260);
            lnkForgot.Name = "lnkForgot";
            lnkForgot.Size = new Size(125, 20);
            lnkForgot.TabIndex = 6;
            lnkForgot.TabStop = true;
            lnkForgot.Text = "Forgot Password?";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(243, 134, 48);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(40, 340);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(280, 42);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(40, 400);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(280, 35);
            btnExit.TabIndex = 8;
            btnExit.Text = "Exit";
            btnExit.Click += btnExit_Click;
            // 
            // LoginForm
            // 
            ClientSize = new Size(1100, 600);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HID Aero Controller";
            Load += LoginForm_Load_1;
            pnlMain.ResumeLayout(false);
            pnlBanner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBanner).EndInit();
            pnlLogin.ResumeLayout(false);
            pnlLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Panel pnlBanner;
        private Panel pnlLogin;
        private PictureBox picBanner;
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
        private LinkLabel lnkForgot;
    }
}