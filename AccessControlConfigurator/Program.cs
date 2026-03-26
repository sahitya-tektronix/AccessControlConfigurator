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
            // ******** VERY VERY IMPORTANT ********
            // Without this your API will NEVER work
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           // Application.Run(new MainForm());

            //----------LOGIN FIRST----------
            using (LoginForm login = new LoginForm())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MainForm());
                }
                else
                {
                    return; // close application
                }
            }
        }
    }
}