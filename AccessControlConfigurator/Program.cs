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

            // TODO: Re-enable token verification once the auth API is stable.
            // --- SAVED TOKEN CHECK (commented out) ---
            //string savedToken = TokenFileManager.GetToken();
            //if (!string.IsNullOrWhiteSpace(savedToken))
            //{
            //    TokenManager.Token = savedToken;
            //    if (VerifyToken())
            //    {
            //        Application.Run(new MainForm());
            //        return;
            //    }
            //    else
            //    {
            //        TokenFileManager.DeleteToken();
            //        TokenManager.Token = null;
            //    }
            //}
            // --- END SAVED TOKEN CHECK ---

            // Show login form (hardcoded admin/123 while API is unavailable)
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