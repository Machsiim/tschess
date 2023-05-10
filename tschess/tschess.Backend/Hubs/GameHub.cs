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
    public class GameHub : Hub
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
            
        }

        //public async Task ChallengeUser()
        //{
        //
        //}


    }
}
