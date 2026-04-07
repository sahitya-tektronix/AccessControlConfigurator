using System;

namespace AccessControlConfigurator.Config
{
    public static class AppConfig
    {
        // =========================
        // SERVER SELECTION
        // =========================
        public static bool UseLiveServer = true;

        // Live Production Server
        private static string LiveServerUrl = "https://teksmartsolutions.com/TekHIDApi/";

        // Local Development Server
        //private static string LocalServerUrl = "http://localhost:5001/api/";
        private static string LocalServerUrl = "https://teksmartsolutions.com/TekHIDApi/";

        // Final Base URL (used everywhere)
        public static string BaseUrl
        {
            get
            {
                return UseLiveServer ? LiveServerUrl : LocalServerUrl;
            }
        }

        // =========================
        // LOGIN SETTINGS
        // =========================
        // If TRUE → login using API
        // If FALSE → local admin login
        public static bool UseApiLogin = false;

        // Local admin credentials (for device configuration software)
        public static string AdminUsername = "admin";
        public static string AdminPassword = "1234";

        // =========================
        // APPLICATION INFO
        // =========================
        public static string ApplicationName = "Access Control Configurator";
        public static string Version = "1.0.0";

        // =========================
        // API ENDPOINTS
        // =========================
        public static string ControllersEndpoint => BaseUrl + "controllers";
        public static string DoorsEndpoint => BaseUrl + "doors";
        public static string EventsEndpoint => BaseUrl + "events";
        public static string LoginEndpoint => BaseUrl + "auth/login";
    }
}