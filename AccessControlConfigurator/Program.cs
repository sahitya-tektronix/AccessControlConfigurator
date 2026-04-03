using AccessControlConfigurator.Helpers;
using System;
using System.Net;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Check for saved token (Remember Me feature)
            string savedToken = TokenFileManager.GetToken();
            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                TokenManager.Token = savedToken;

                // Try to verify token by calling a protected API
                if (VerifyToken())
                {
                    Application.Run(new MainForm());
                    return;
                }
                else
                {
                    TokenFileManager.DeleteToken();
                    TokenManager.Token = null;
                }
            }

            // Show login form
            using (LoginForm login = new LoginForm())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MainForm());
                }
                else
                {
                    return;
                }
            }
        }

        private static bool VerifyToken()
        {
            try
            {
                var userService = new Services.UserService();
                var task = userService.GetUsers();
                task.Wait(5000);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}