using AccessControlSystem.ApiClient.AccessControlSystem.ApiClient;
using AccessControlSystem.Models;
using AccessControlSystem.Models.AccessLevel;
using AccessControlSystem.Models.AccessLevelDto;
using AccessControlSystem.Models.AccessLevelDto.AccessLevelDto;
using AccessControlSystem.Models.Acr;
using AccessControlSystem.Models.Cards;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;




namespace AccessControlSystem.Services
{
    public class ApiService
    {
        
        //private readonly HttpClient _http;
        // private WebSocketService _ws;
        public event Action<EventDto> OnLiveEvent;

        private readonly HttpClient _httpClient;
        private readonly HttpClient _client = new HttpClient();

       // private string _baseUrl = "https://teksmartsolutions.com/TekHIDApi/";
        //private string _baseUrl = "https://teksmartsolutions.com/TekHIDApi/";
        private string _baseUrl = "https://teksmartsolutions.com/TekHIDAPI/";

        private string _token = "";
        private int _selectedControllerId = 0;
        private object response;
        private readonly string BaseUrl = "http://localhost:7010/";


        public ApiService()
        {


            //_http = new HttpClient();
            _httpClient = new HttpClient();

    _httpClient.BaseAddress = new Uri("https://teksmartsolutions.com/TekHIDApi/");

    _httpClient.Timeout = TimeSpan.FromSeconds(30);

    _httpClient.DefaultRequestHeaders.Accept.Clear();

    _httpClient.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //_http.BaseAddress = new Uri("https://localhost:5001/api/");
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                // ================= LOCAL LOGIN =================
                if (!AppConfig.UseApiLogin)
                {
                    // Offline login
                    if (username == "admin" && password == "123")
                        return true;

                    return false;
                }

                // ================= REAL API LOGIN =================
                _httpClient.DefaultRequestHeaders.Authorization = null;

                var loginObj = new
                {
                    username = username,
                    password = password
                };

                var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginObj);

                var raw = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return false;

                var result = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(raw,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (result == null || string.IsNullOrWhiteSpace(result.token))
                    return false;

                _token = result.token;

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public class LoginResponse
        {
        private int _selectedControllerId;

        public string token { get; set; }
        }

        public async Task<List<ControllerDto>> GetControllersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/hid/GetAllControllers?locationID=0");

                if (!response.IsSuccessStatusCode)
                    return new List<ControllerDto>();

                return await response.Content.ReadFromJsonAsync<List<ControllerDto>>();
            }
            catch
            {
                return new List<ControllerDto>();
            }
        }

        public async Task<ControllerDto> GetControllerById(int id)
        {
            var response = await _httpClient.GetAsync($"api/hid/GetControllerById/{id}");

            var json = await response.Content.ReadAsStringAsync();

            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true   // 🔥 IMPORTANT
            };

            return System.Text.Json.JsonSerializer.Deserialize<ControllerDto>(json, options);
        }
        // Save selected controller
        public void SetSelectedController(int controllerId)
        {
            _selectedControllerId = controllerId;
        }


        public async Task<List<DoorDto>> GetDoorsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"api/controllers/{_selectedControllerId}/doors");

                if (!response.IsSuccessStatusCode)
                    return new List<DoorDto>();

                return await response.Content.ReadFromJsonAsync<List<DoorDto>>();
            }
            catch
            {
                return new List<DoorDto>();
            }
        }

        public async Task<List<EventDto>> GetEventsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/events");

                if (!response.IsSuccessStatusCode)
                    return new List<EventDto>();

                return await response.Content.ReadFromJsonAsync<List<EventDto>>();
            }
            catch
            {
                return new List<EventDto>();
            }
        }

        public async Task<bool> OpenDoorAsync(int doorId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"api/doors/{doorId}/open", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        private void Ws_OnMessageReceived(string message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("RAW WS: " + message);

                // ignore ping packets
                if (string.IsNullOrWhiteSpace(message) || message.Length < 5)
                    return;

                // TekHID sends JSON
                var evt = Newtonsoft.Json.JsonConvert.DeserializeObject<EventDto>(message);

                if (evt != null)
                {
                    OnLiveEvent?.Invoke(evt);
                }
            }
            catch
            {
                // many packets are heartbeat packets -> ignore
            }
        }

        //need to change api
        public async Task<bool> ConfigureOnBoardAsync(int controllerId)
        {
            var response = await _httpClient.PostAsync(
                $"api/controllers/{controllerId}/onboard",
                null);

            return response.IsSuccessStatusCode;
        }
        public async Task DeleteControllerAsync(int controllerId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var response = await client.DeleteAsync($"api/controllers/{controllerId}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Unable to delete controller");
            }
        }
        public async Task<List<SioModel>> GetSiosAsync(int controllerId)
        {
            try
            {
                string url = $"api/controllers/{controllerId}/sios";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    throw new Exception("Server error: " + error);
                }

                var json = await response.Content.ReadAsStringAsync();

                return System.Text.Json.JsonSerializer.Deserialize<List<SioModel>>(
                    json,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load SIOs: " + ex.Message);
            }
        }
        public async Task<bool> SaveSioBulkAsync(int controllerId, SioBulkConfigDto request)
        {
            try
            {
                string url = $"api/controllers/{controllerId}/sios/bulk";

                var response = await _httpClient.PostAsJsonAsync(url, request);

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    throw new Exception("Server error: " + error);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save SIO settings: " + ex.Message);
            }
        }
        public async Task<List<SioItemDto>> GetControllerSiosAsync(int controllerId)
        {
            try
            {
                string url = $"api/controllers/{controllerId}/sios";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return new List<SioItemDto>();

                var result = await response.Content.ReadFromJsonAsync<List<SioItemDto>>();

                return result ?? new List<SioItemDto>();
            }
            catch
            {
                return new List<SioItemDto>();
            }
        }


        public async Task DeleteSioAsync(int controllerId, int sioId)
        {
            var response = await _httpClient.DeleteAsync(
                $"api/controllers/{controllerId}/sios/{sioId}");

            response.EnsureSuccessStatusCode();

        }
        public async Task<List<DiscoverControllerDto>> DiscoverControllers()
        {
            string url = "api/hid/GetAllDiscoverdControllers";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<DiscoverControllerDto>();

            var controllers = await response.Content.ReadFromJsonAsync<List<DiscoverControllerDto>>();

            return controllers ?? new List<DiscoverControllerDto>();
        }

        // ================= SYNC CONTROLLERS =================
        public async Task<bool> SyncControllersToHID()
        {
            try
            {
                //https://teksmartsolutions.com/TekHIDAPI/api/hid/SyncControllersToHID
                var response = await _httpClient.PostAsync("api/hid/SyncControllersToHID", null);

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> SyncControllersOnlineStatus()
        {
            try
            {
                string url = "api/hid/SyncControllersOnlineStatus";

                var response = await _httpClient.PostAsync(url, null);

                if (!response.IsSuccessStatusCode)
                    return "Sync failed";

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //public async Task<List<ControllerStatusDto>> GetControllersOnlineStatus()
        //{
        //    var response = await _httpClient.GetAsync("api/hid/SyncControllersOnlineStatus");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadFromJsonAsync<List<ControllerStatusDto>>();
        //    }

        //    return new List<ControllerStatusDto>();
        //}


        //ACR

        //post
        public async Task CreateAcrAsync(int controllerId, int sioNumber, AcrDto acr)
        {
            string url = $"api/controllers/{controllerId}/sios/{sioNumber}/acrs";

            var response = await _httpClient.PostAsJsonAsync(url, acr);

            response.EnsureSuccessStatusCode();
        }

        //put
        public async Task UpdateAcrAsync(int controllerId, int sioNumber, int id, AcrDto acr)//rex0Number
        {
            string url = $"api/controllers/{controllerId}/sios/{sioNumber}/acrs/{id}";

            var response = await _httpClient.PutAsJsonAsync(url, acr);

            response.EnsureSuccessStatusCode();
        }
   
        public async Task<bool> DeleteAcrAsync(int controllerId, int sioNumber, int acrId)
        {
            try
            {
                string url = $"api/controllers/{controllerId}/sios/{sioNumber}/acrs/{acrId}";

                var response = await _httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<AcrDto>> GetAcrs(int controllerId, int sioNumber)
        {
            string url = $"api/controllers/{controllerId}/sios/{sioNumber}/acrs";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }

            var result = await response.Content.ReadFromJsonAsync<List<AcrDto>>();

            return result ?? new List<AcrDto>();
        }
        public async Task<List<AcrDropdownDto>> GetAllAcrDropdown()
        {
            try
            {
                string url = "Api/Acrs/GetAllDropdown";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<AcrDropdownDto>>();

                return result ?? new List<AcrDropdownDto>();
            }
            catch (Exception ex)
            {
                throw new Exception("API Error: " + ex.Message);
            }
        }
        public async Task<List<AcrDto>> GetAcrSearchAsync(string name)
        {
            var request = new
            {
                controllerIds = new int[] { },
                sioNumbers = new int[] { },
                acrNumbers = new int[] { },
                readerDirections = new int[] { },
                name = name,
                pageNumber = 1,
                pageSize = 100
            };

            var response = await _httpClient.PostAsJsonAsync(
                "Api/Acrs/Search",
                request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AcrSearchResponse>();

            return result.data;
        }

        //AccessLevel
        public async Task<List<AccessLevelDto>> GetAccessLevels()
        {
            var response = await _httpClient.GetAsync("api/access-levels?includeDeleted=false");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<AccessLevelDto>>();
                return result.data ?? new List<AccessLevelDto>();
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"API Error: {error}");
        }
        public async Task<bool> AddAccessLevel(AccessLevelCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/access-levels", dto);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"API Error: {error}");
        }

        public async Task UpdateAccessLevel(int id, AccessLevelCreateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/access-levels/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
        public async Task DeleteAccessLevel(int id)
        {
            await _httpClient.DeleteAsync($"api/access-levels/{id}");
        }

        public async Task SyncAccessLevels()
        {
            await _httpClient.PostAsync("api/access-levels/SyncAccessLevelsToHID", null);
        }
        //Timezones
        public async Task<List<TimezoneDto>> GetTimezones()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TimezoneDto>>(
                "api/timezones");

            return response ?? new List<TimezoneDto>();
        }
        


        public async Task CreateTimezone(TimezoneDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/timezones", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateTimezone(int id, TimezoneDto dto)
    {
        await _httpClient.PutAsJsonAsync($"api/timezones/{id}", dto);
    }

    public async Task DeleteTimezone(int id)
    {
        await _httpClient.DeleteAsync($"api/timezones/{id}");
    }

    public async Task SyncTimezonesToHID()
    {
        await _httpClient.PostAsync("api/timezones/SyncTimezonesToHID", null);
    }

        public async Task ApplyHoliday(HolidayApplyDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/timezones/holidays/apply", dto);

            response.EnsureSuccessStatusCode();
        }
        public async Task<object> QueryHoliday(int scpId)
        {
            var request = new
            {
                scpId = scpId,
                queryType = "all",
                first = 0,
                count = 100
            };

            var response = await _httpClient.PostAsJsonAsync("api/timezones/query", request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<object>();
        }
        public async Task<List<TimezoneDto>> GetTimezonesDropdown()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TimezoneDto>>(
                "api/timezones/GetTimezonesDropdown");

            return response ?? new List<TimezoneDto>();
        }
        public async Task<List<AcrDto>> GetAcrs()
        {
            var response = await _httpClient.GetAsync("/api/acrs");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {error}");
            }

            var data = await response.Content.ReadFromJsonAsync<List<AcrDto>>();

            return data ?? new List<AcrDto>();
        }


        //Cards Api

        public async Task<List<CardDto>> GetCards()
        {
            var response = await _httpClient.GetAsync("api/cards");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<CardDto>>();

            return data ?? new List<CardDto>();
        }
        public async Task<bool> CreateCard(CreateCardDto card)
        {
            var response = await _httpClient.PostAsJsonAsync("api/cards", card);

            if (response.IsSuccessStatusCode)
                return true;

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }


        public async Task<bool> UpdateCard(int id, UpdateCardDto card)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/cards/{id}", card);

           
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteCard(long cardNumber)
        {
            var response = await _httpClient.DeleteAsync($"api/cards/{cardNumber}");

            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> SyncCardsToHID()
        {
            var response = await _httpClient.PostAsync("api/cards/SyncCardsToHID", null);

            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        // CardHolder
        public async Task<List<CardholderDto>> GetCardholders()
        {
            var response = await _httpClient.GetAsync("api/cardholders/listofcardholders");

            if (!response.IsSuccessStatusCode)
                return new List<CardholderDto>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CardholderDto>>(json);
        }

        public async Task<CardholderDto> GetCardholderByUserId(int userId)
        {
            var response = await _httpClient.GetAsync($"api/cardholders/detailsbyuserid/{userId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CardholderDto>(json);
        }

        public async Task<bool> CreateCardholder(CardholderDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/cardholders/create", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }

            return true;
        }
        public async Task<bool> UpdateCardholder(int userId, UpdateCardholderRequest dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/cardholders/update/{userId}", dto);

            var result = await response.Content.ReadAsStringAsync();

            // 🔥 SHOW ACTUAL SERVER RESPONSE
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }

            return true;
        }
    }
      
    }

    




        public class LoginResponse
    {
        public string token { get; set; }
    }




    public class DoorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string Status { get; set; }
    }

    public class EventDto
    {
        public string CardNumber { get; set; }
        public string PersonName { get; set; }
        public string DoorName { get; set; }
        public DateTime Time { get; set; }
        public string Result { get; set; }
    }

