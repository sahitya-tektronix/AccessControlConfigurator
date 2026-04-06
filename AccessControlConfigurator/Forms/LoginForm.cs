using AccessControlConfigurator.DTOs;
using AccessControlConfigurator.Forms;
using AccessControlConfigurator.Helpers;
using AccessControlConfigurator.Services;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService = new AuthService();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Enter Username", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Enter Password", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Logging in...";
            this.Cursor = Cursors.WaitCursor;

            var request = new LoginRequestDto
            {
                Username = username,
                Password = password
            };

            try
            {
                var result = await _authService.Login(request);

                if (result != null && !string.IsNullOrWhiteSpace(result.Token))
                {
                    TokenManager.Token = result.Token;
                    UserSession.UserId = result.Id;
                    UserSession.Username = result.Username;
                    UserSession.Role = result.Role;

                    if (chkRememberMe.Checked)
                    {
                        TokenFileManager.SaveToken(result.Token);
                    }

                    MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Cannot connect to server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Login";
                this.Cursor = Cursors.Default;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            using (ResetPasswordForm resetForm = new ResetPasswordForm())
            {
                resetForm.ShowDialog(this);
            }
        }

        private void lnkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnForgotPassword_Click(null, null);
        }

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