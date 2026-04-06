using AccessControlConfigurator.DTOs;
using AccessControlConfigurator.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccessControlConfigurator.Services
{
    public class CardholderService : BaseApiService
    {
        public async Task<List<CardholderDto>> GetCardholders()
        {
            SetAuthorizationHeader();

            try
            {
                var response = await HttpClient.GetAsync("api/cardholders/listofcardholders");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    TokenManager.Token = null;
                    UserSession.Clear();
                    TokenFileManager.DeleteToken();
                    throw new TokenExpiredException("Session expired. Please login again.");
                }

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var cardholders = JsonSerializer.Deserialize<List<CardholderDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return cardholders ?? new List<CardholderDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Cannot connect to server: {ex.Message}", ex);
            }
        }
    }
}