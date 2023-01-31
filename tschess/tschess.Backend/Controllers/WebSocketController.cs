using Microsoft.AspNetCore.Mvc;
using tschess.Application.Services;

namespace tschess.Backend.Controllers
{
    public class WebSocketController : ControllerBase
    {

        private readonly WebsocketService _websocketService;

        public WebSocketController(WebsocketService websocketService)
        {
            _websocketService = websocketService;
        }

        [HttpGet("/ws/{clientGuid:Guid}")]
        public async Task Get(Guid clientGuid)
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await _websocketService.DoWork(clientGuid, webSocket);
        }


    }
}
