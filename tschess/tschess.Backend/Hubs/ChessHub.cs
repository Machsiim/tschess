using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using MimeKit.Encodings;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Tschess.Application.Infrastructure;
using Tschess.Application.Model;

namespace Tschess.Backend.Hubs
{
    [Authorize]
    public class ChessHub : Hub
    {

        public static ConcurrentBag<string> ConnectedUsers = new ConcurrentBag<string>();
        public TschessContext _db;



        /// <summary>
        /// Sends the current username from the token to the client.
        /// </summary>
        /// <returns></returns>
        /// 

        public ChessHub(TschessContext db) {
            _db = db;
        }

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
            //if(ConnectedUsers.Contains(Context.User.Identity.Name)) {  return; }
            ConnectedUsers.Add(Context.User.Identity.Name);
            await Clients.All.SendAsync("SetWaitingroomState", ConnectedUsers);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User?.Identity?.Name is null) { return; }
            ConnectedUsers = new ConcurrentBag<string>(ConnectedUsers.Except(new[] { Context.User?.Identity?.Name }));
            await Clients.All.SendAsync("SetWaitingroomState", ConnectedUsers);
        }

        public async Task ChallengeUser(string challenged)
        {
            string [] users = { Context.User?.Identity?.Name, challenged };
            string usersString = Context.User?.Identity?.Name + challenged;
            Groups.AddToGroupAsync(Context.ConnectionId, usersString);
            await Clients.All.SendAsync("GetChallenges", users);
            Console.WriteLine(users.ToString());
        }

        public async Task LeaveWaitingroom()
        {
            if (Context.User?.Identity?.Name is null) { return; }
            ConnectedUsers = new ConcurrentBag<string>(ConnectedUsers.Except(new[] { Context.User?.Identity?.Name }));
            await Clients.All.SendAsync("SetWaitingroomState", ConnectedUsers);
        }

        public async Task StartGame(string challenger)
        {
            string challenged = Context.User.Identity.Name;
            
            string users = challenger + challenged;
            Groups.AddToGroupAsync(Context.ConnectionId, users);
            
            Game game = new Game(player1: challenger, player2: challenged);
            _db.Games.Add(game);
            try { _db.SaveChanges(); }
            catch { return; }

            await Clients.Group(users).SendAsync("GameStarted", game.Guid.ToString());

            ConnectedUsers = new ConcurrentBag<string>(ConnectedUsers.Except(new[] { challenged, challenger }));
            await Clients.All.SendAsync("SetWaitingroomState", ConnectedUsers);

        }

        public async Task SetGameState(Guid GameGuid, string fen)
        {
            var game = _db.Games.FirstOrDefault(g => g.Guid == GameGuid);
            if (game is null) return;
            game.GameState = fen;
            try { _db.SaveChanges(); } catch {  return; }

            string users = game.Player1 + game.Player2;
            if(Context.User?.Identity?.Name == game.Player1) await Clients.Group(users).SendAsync("SetGameState", new string[] { fen, game.Player2 });
            else if (Context.User?.Identity?.Name == game.Player2) await Clients.Group(users).SendAsync("SetGameState", new string[] { fen, game.Player1 });

        }

        public async Task GetGameState(Guid GameGuid)
        {
            var game = _db.Games.FirstOrDefault(g => g.Guid == GameGuid);
            if (game is null) return;
            string users = game.Player1 + game.Player2;
            await Clients.Caller.SendAsync("SetGameState", new string[] { game.GameState, game.Player2 });
        }

        public async Task EndGame(Guid GameGuid, string winner)
        {
            var game = _db.Games.FirstOrDefault(g => g.Guid == GameGuid);
            if (game is null) return;
            string users = game.Player1 + game.Player2;
            await Clients.Group(users).SendAsync("GameEnd", winner);
        }

    }
}
