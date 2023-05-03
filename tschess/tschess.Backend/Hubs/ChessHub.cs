using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tschess.Backend.Hubs
{
    [Authorize]
    public class ChessHub : Hub
    {

        public static readonly ConcurrentBag<string> ConnectedUsers = new ConcurrentBag<string>();

        /// <summary>
        /// Sends the current username from the token to the client.
        /// </summary>
        /// <returns></returns>
        /// 
        public override async Task OnConnectedAsync()
        {
            var group = Context.User?.Claims.FirstOrDefault(c => c.Type == "Group")?.Value;

            await Clients.All.SendAsync("ReceiveMessage",
                $"{Context.User?.Identity?.Name} in Group {group} joined.");
        }
        /// <summary>
        /// Invoked by
        ///     connection.invoke("SendMessage", message);
        /// in Javascript SignalR client.
        /// </summary>
        public async Task SendMessage(string message)
        {
            // Can be received with
            //     connection.on("ReceiveMessage", callback);
            // in Javascript SignalR client.
            await Clients.All.SendAsync("ReceiveMessage", message);

        }

        public async Task EnterWaitingroom()
        {
            await Clients.Caller.SendAsync("Warteraum beigetreten");
            await GetWaitingroom();
            ConnectedUsers.Add(Context.User?.Identity?.Name);
            await Clients.All.SendAsync("WaitForPlayers", Context.User?.Identity?.Name);
        }

        public async Task GetWaitingroom()
        {
            foreach(string user in ConnectedUsers)
            {
                await Clients.Caller.SendAsync($"{user}");
            }
        }


    }
}
