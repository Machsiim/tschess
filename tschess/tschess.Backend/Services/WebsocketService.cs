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
        
    }
}
