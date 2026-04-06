using System;
using System.IO;

namespace AccessControlConfigurator.Helpers
{
    public static class TokenFileManager
    {
        private static readonly string _tokenFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AccessControlConfigurator",
            "token.txt"
        );

        public static void SaveToken(string token)
        {
            try
            {
                string directory = Path.GetDirectoryName(_tokenFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(_tokenFilePath, token);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving token: {ex.Message}");
            }
        }

        public static string GetToken()
        {
            try
            {
                if (File.Exists(_tokenFilePath))
                {
                    return File.ReadAllText(_tokenFilePath).Trim();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading token: {ex.Message}");
            }

            return null;
        }

        public static bool TokenFileExists()
        {
            return File.Exists(_tokenFilePath);
        }

        public static void DeleteToken()
        {
            try
            {
                if (File.Exists(_tokenFilePath))
                {
                    File.Delete(_tokenFilePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting token: {ex.Message}");
            }
        }
    }
}