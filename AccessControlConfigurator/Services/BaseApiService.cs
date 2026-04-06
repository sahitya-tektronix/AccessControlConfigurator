using AccessControlConfigurator.Helpers;
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
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/")
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