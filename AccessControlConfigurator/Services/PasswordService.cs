using AccessControlConfigurator.DTOs;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccessControlConfigurator.Services
{
    public class PasswordService : BaseApiService
    {
        public async Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                var response = await HttpClient.PostAsJsonAsync("api/auth/reset-password", request);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new InvalidOperationException("Invalid username or request format");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("Unauthorized");

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var resetResponse = JsonSerializer.Deserialize<ResetPasswordResponseDto>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return resetResponse ?? new ResetPasswordResponseDto { Success = false, Message = "Unknown error" };
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Cannot connect to server: {ex.Message}", ex);
            }
        }
    }
}