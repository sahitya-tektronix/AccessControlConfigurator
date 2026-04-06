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
    public class UserService : BaseApiService
    {
        public async Task<List<UserDto>> GetUsers()
        {
            SetAuthorizationHeader();

            try
            {
                var response = await HttpClient.GetAsync("api/auth/users");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    TokenManager.Token = null;
                    UserSession.Clear();
                    TokenFileManager.DeleteToken();
                    throw new TokenExpiredException("Session expired. Please login again.");
                }

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<List<UserDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return users ?? new List<UserDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Cannot connect to server: {ex.Message}", ex);
            }
        }
    }
}