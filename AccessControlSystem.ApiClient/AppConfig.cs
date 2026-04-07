namespace AccessControlSystem.ApiClient
{
    public static class AppConfig
    {
        private static readonly AppSettingsService _settings = new AppSettingsService();

        public static string ApiBaseUrl => _settings.ApiBaseUrl;

        public static string WebSocketUrl => _settings.WebSocketUrl;

        public static string LicenseKey => _settings.LicenseKey;

        public static void SaveLicenseKey(string key) => _settings.SaveLicenseKey(key);

        public static void ResetLicenseKey() => _settings.ResetLicenseKey();

        public static bool UseApiLogin { get; set; } = false;

        /// <summary>
        /// Stores the active bearer token so all new ApiService instances can pick it up.
        /// Set this immediately after a successful login.
        /// </summary>
        public static string Token { get; set; } = string.Empty;
    }
}
