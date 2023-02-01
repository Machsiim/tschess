using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using tschess.Application.Services;
using HashidsNet;

namespace tschess.Backend.Controllers
{
    public class WebSocketController : ControllerBase
    {

        private readonly WebsocketService _websocketService;
        private readonly IConfiguration _config;

        public WebSocketController(WebsocketService websocketService, IConfiguration config)
        {
            _websocketService = websocketService;
            _config = config;
        }

        /// <summary>
        /// GET /api/ws/clientid
        /// </summary>
        /// <param name="clientGuid"></param>
        /// <returns></returns>
        [HttpGet("/ws/clientid/{userid:int}")]
        public IActionResult Hash(int userid)
        {
            var hashids = new Hashids(_config["HashidSalt"]);
            var hash = hashids.Encode(userid);
            return Ok(hash);
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
