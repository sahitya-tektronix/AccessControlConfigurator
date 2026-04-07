using System;
using System.IO;
using System.Text.Json;

namespace AccessControlSystem.ApiClient
{
    public class AppSettingsService
    {
        private readonly string _filePath;

        public AppSettingsService()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            EnsureFileExists();
        }

        public string ApiScheme
        {
            get
            {
                var model = Load();
                return string.IsNullOrWhiteSpace(model.ConnectionSettings.ApiScheme)
                    ? "https"
                    : model.ConnectionSettings.ApiScheme.Trim().ToLower();
            }
        }

        public string HostPath
        {
            get
            {
                var model = Load();
                return NormalizeHostPath(model.ConnectionSettings.HostPath);
            }
        }

        public string ApiBaseUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(HostPath))
                    return string.Empty;

                return $"{ApiScheme}://{HostPath}";
            }
        }

        public string WebSocketUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(HostPath))
                    return string.Empty;

                string wsScheme = ApiScheme == "http" ? "ws" : "wss";
                return $"{wsScheme}://{HostPath.TrimEnd('/')}/ws";
            }
        }

        public string LicenseKey
        {
            get
            {
                var model = Load();
                return model.LicenseKey ?? string.Empty;
            }
        }

        public void Save(string scheme, string hostPath)
        {
            var model = Load();
            model.ConnectionSettings = new ConnectionSettings
            {
                ApiScheme = NormalizeScheme(scheme),
                HostPath  = NormalizeHostPath(hostPath)
            };
            WriteModel(model);
        }

        public void SaveLicenseKey(string key)
        {
            var model = Load();
            model.LicenseKey = key?.Trim() ?? string.Empty;
            WriteModel(model);
        }

        public void ResetLicenseKey()
        {
            var model = Load();
            model.LicenseKey = string.Empty;
            WriteModel(model);
        }

        public void Reset()
        {
            Save("https", "");
        }

        private AppSettingsRoot Load()
        {
            EnsureFileExists();
            try
            {
                string json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                    return CreateDefaultModel();

                return JsonSerializer.Deserialize<AppSettingsRoot>(json) ?? CreateDefaultModel();
            }
            catch
            {
                return CreateDefaultModel();
            }
        }

        private void WriteModel(AppSettingsRoot model)
        {
            string json = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(_filePath))
                WriteModel(CreateDefaultModel());
        }

        private static AppSettingsRoot CreateDefaultModel() =>
            new AppSettingsRoot
            {
                ConnectionSettings = new ConnectionSettings { ApiScheme = "https", HostPath = "" },
                LicenseKey = ""
            };

        private static string NormalizeScheme(string scheme)
        {
            scheme = (scheme ?? "").Trim().ToLower();
            return scheme == "http" ? "http" : "https";
        }

        private static string NormalizeHostPath(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            string result = value.Trim();
            foreach (var prefix in new[] { "https://", "http://", "wss://", "ws://" })
            {
                if (result.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    result = result.Substring(prefix.Length);
                    break;
                }
            }

            if (!result.EndsWith("/"))
                result += "/";

            return result;
        }

        private class AppSettingsRoot
        {
            public ConnectionSettings ConnectionSettings { get; set; } = new ConnectionSettings();
            public string LicenseKey { get; set; } = "";
        }

        private class ConnectionSettings
        {
            public string ApiScheme { get; set; } = "https";
            public string HostPath  { get; set; } = "";
        }
    }
}
