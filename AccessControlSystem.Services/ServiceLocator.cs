using System;
using static System.Net.WebRequestMethods;

namespace AccessControlSystem.Services
{
    public static class ServiceLocator
    {
        private static ApiService _api;

        public static ApiService Api
        {
            get
            {
                if (_api == null)
                    _api = new ApiService();
                ////_http.Timeout = TimeSpan.FromMinutes(3);

                return _api;
            }
        }
    }
}