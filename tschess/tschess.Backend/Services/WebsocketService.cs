using System.Collections.Concurrent;
using System.Net.WebSockets;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using Microsoft.Extensions.Logging;
namespace tschess.Application.Services
{
    public class WebsocketService
    {
        private readonly ILogger<WebsocketService> _logger;

        public WebsocketService(ILogger<WebsocketService> logger)
        {
            _logger = logger;
        }
        private readonly ConcurrentDictionary<Guid, WebSocket> _clients = new();

        

        public async Task DoWork(Guid clientGuid, WebSocket webSocket)
        {
            _clients.TryAdd(clientGuid, webSocket);
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            _clients.TryRemove(clientGuid, out _);
        }

    }
}
