using System;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class LoginForm : Form
    {
        // 👇 HARDCODED LOGIN (temporary)
        private const string VALID_USERNAME = "admin";
        private const string VALID_PASSWORD = "123";

        public LoginForm()
        {
            InitializeComponent();
        }

        // FORM LOAD
        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        // LOGIN BUTTON
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Empty validation
            if (username == "")
            {
                MessageBox.Show("Enter Username");
                txtUsername.Focus();
                return;
            }

            if (password == "")
            {
                MessageBox.Show("Enter Password");
                txtPassword.Focus();
                return;
            }

            // Authentication
            if (username == VALID_USERNAME && password == VALID_PASSWORD)
            {
                MessageBox.Show("Login Successful", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // tell Program.cs login success
                this.DialogResult = DialogResult.OK;

                // close login form
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        // EXIT BUTTON
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ENTER KEY SUPPORT
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin.PerformClick();
        }

        private void LoginForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}