using AccessControlConfigurator.Helpers;
using AccessControlSystem.ApiClient;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AccessControlConfigurator.Services
{
    public class BaseApiService
    {
        private static readonly HttpClient _httpClient;

        static BaseApiService()
        {
            string baseUrl = AppConfig.ApiBaseUrl;
            if (string.IsNullOrWhiteSpace(baseUrl))
                baseUrl = "https://teksmartsolutions.com/TekHIDApi/";

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        protected HttpClient HttpClient => _httpClient;

        protected void SetAuthorizationHeader()
        {
            if (!string.IsNullOrWhiteSpace(TokenManager.Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
    }
}