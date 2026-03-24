using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AccessControlSystem.Services
{
    public class WebSocketService
    {
        private ClientWebSocket _socket;
        private CancellationTokenSource _cts;

        public event Action<string> OnMessageReceived;
        public event Action<string> OnError;
        public WebSocketState State
        {
            get
            {
                if (_socket == null)
                    return WebSocketState.Closed;

                return _socket.State;
            }
        }
        public async Task SendAsync(string message)
        {
            if (_socket == null || _socket.State != WebSocketState.Open)
                throw new Exception("WebSocket is not connected.");

            var bytes = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(bytes);

            await _socket.SendAsync(
                segment,
                WebSocketMessageType.Text,
                true,
                _cts.Token);
        }


        public async Task<bool> ConnectAsync(string url)
        {
            _cts = new CancellationTokenSource();
            _socket = new ClientWebSocket();

            // ✅ FIX SSL HERE (THIS IS THE REAL FIX)
            _socket.Options.RemoteCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;

            _socket.Options.SetRequestHeader("ClientType", "Configurator");

            // ❌ REMOVE THIS FOR LOCAL
            // _socket.Options.SetRequestHeader("Origin", "https://teksmartsolutions.com");

            try
            {
                await _socket.ConnectAsync(new Uri(url), _cts.Token);

                if (_socket.State == WebSocketState.Open)
                {
                    _ = Task.Run(ReceiveLoop);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.ToString());
                return false;
            }
        }

        private async Task ReceiveLoop()
        {
            var buffer = new byte[8192];

            try
            {
                while (_socket.State == WebSocketState.Open && !_cts.IsCancellationRequested)
                {
                    using (var ms = new MemoryStream())
                    {
                        WebSocketReceiveResult result;

                        do
                        {
                            result = await _socket.ReceiveAsync(
                                new ArraySegment<byte>(buffer),
                                _cts.Token);

                            if (result.MessageType == WebSocketMessageType.Close)
                            {
                                await DisconnectAsync();
                                return;
                            }

                            ms.Write(buffer, 0, result.Count);

                        } while (!result.EndOfMessage);

                        var message = Encoding.UTF8.GetString(ms.ToArray());

                        System.Diagnostics.Debug.WriteLine("WS EVENT: " + message);
                        OnMessageReceived?.Invoke(message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Normal shutdown
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        public async Task DisconnectAsync()
        {
            try
            {
                _cts?.Cancel();

                if (_socket != null && _socket.State == WebSocketState.Open)
                {
                    await _socket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        "Client closing",
                        CancellationToken.None);
                }
            }
            catch { }
            finally
            {
                _socket?.Dispose();
                _socket = null;
            }
        }
    }
}


