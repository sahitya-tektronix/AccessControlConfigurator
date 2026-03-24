using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.ApiClient
{
    namespace AccessControlSystem.ApiClient
    {
        public static class AppConfig
        {
            // CHANGE PORT IF YOUR API USES DIFFERENT
            public static string BaseUrl = "http://localhost:5000/api/";

            public static bool UseApiLogin { get; internal set; }
        }
    }
}
