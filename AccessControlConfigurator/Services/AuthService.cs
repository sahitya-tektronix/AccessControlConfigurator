using AccessControlConfigurator.DTOs;
using AccessControlConfigurator.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccessControlConfigurator.Services
{
    public class AuthService : BaseApiService
    {
        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                var response = await HttpClient.PostAsJsonAsync("api/auth/login", request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("Invalid credentials");

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new InvalidOperationException("Invalid username or password format");

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponseDto>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (loginResponse == null)
                {
                    throw new Exception("Unable to deserialize login response");
                }

                return loginResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Cannot connect to server: {ex.Message}", ex);
            }
        }
    }
}