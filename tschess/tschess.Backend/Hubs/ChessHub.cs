using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tschess.Backend.Hubs
{
    [Authorize]
    public class ChessHub : Hub
    {

        public static ConcurrentBag<string> ConnectedUsers = new ConcurrentBag<string>();

        /// <summary>
        /// Sends the current username from the token to the client.
        /// </summary>
        /// <returns></returns>
        /// 
        public override async Task OnConnectedAsync()
        {
            var group = Context.User?.Claims.FirstOrDefault(c => c.Type == "Group")?.Value;

            //await Clients.All.SendAsync("ReceiveMessage",
            //    $"{Context.User?.Identity?.Name} in Group {group} joined.");
        }
        /// <summary>
        /// Invoked by
        ///     connection.invoke("SendMessage", message);
        /// in Javascript SignalR client.h
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
            if (Context.User?.Identity?.Name is null) { return; }
            ConnectedUsers.Add(Context.User.Identity.Name);
            await Clients.All.SendAsync("SetWaitingroomState", ConnectedUsers);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User?.Identity?.Name is null) { return; }
            ConnectedUsers = new ConcurrentBag<string>(ConnectedUsers.Except(new[] { Context.User?.Identity?.Name }));
            await Clients.All.SendAsync("SetWaitingroomState", ConnectedUsers);
        }

    }
}
