
using AccessControlSystem.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AccessControlSystem.Services
{
    public class ControllerDiscoveryService
    {
        // Try common controller ports (VERY IMPORTANT)
        private readonly int[] _ports = new[] { 80, 8080, 3001, 4070 };

        // SINGLE HttpClient (fixes socket disconnect)
        private static readonly HttpClient _http = new HttpClient()
        {
            Timeout = TimeSpan.FromMilliseconds(700)
        };

        // limit parallel scanning (prevents network freeze)
        private readonly SemaphoreSlim _throttle = new SemaphoreSlim(25);

        public async Task<List<ControllerDto>> DiscoverAsync()
        {
            var foundControllers = new ConcurrentBag<ControllerDto>();

            string subnet = "192.168.10.";   // change if needed

            List<Task> tasks = new List<Task>();

            for (int i = 1; i <= 254; i++)
            {
                string ip = subnet + i;
                tasks.Add(ScanIpAsync(ip, foundControllers));
            }

            await Task.WhenAll(tasks);

            return new List<ControllerDto>(foundControllers);
        }

        private async Task ScanIpAsync(string ip, ConcurrentBag<ControllerDto> result)
        {
            await _throttle.WaitAsync();

            try
            {
                // Step 1: Ping
                using (Ping ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(ip, 250);

                    if (reply.Status != IPStatus.Success)
                        return;
                }

                // Step 2: Try each controller port
                foreach (var port in _ports)
                {
                    var controller = await TryGetController(ip, port);
                    if (controller != null)
                    {
                        result.Add(controller);
                        return; // stop after first match
                    }
                }
            }
            catch
            {
                // ignore unreachable devices
            }
            finally
            {
                _throttle.Release();
            }
        }

        private async Task<ControllerDto> TryGetController(string ip, int port)
        {
            try
            {
                string url = $"http://{ip}:{port}/device/info";

                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();

                var device = JsonSerializer.Deserialize<DeviceInfo>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (device == null || string.IsNullOrEmpty(device.mac))
                    return null;

                return new ControllerDto
                {
                    Name = string.IsNullOrEmpty(device.model)
                            ? $"Controller [{device.mac}]"
                            : device.model,

                    IpAddress = ip,
                    MacAddress = device.mac,
                    Status = 1,
                    IsEnabled = true
                };
            }
            catch
            {
                return null;
            }
        }
    }

    public class DeviceInfo
    {
        public string model { get; set; }
        public string mac { get; set; }
    }
}