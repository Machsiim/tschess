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
        public static ConcurrentBag<string> UsersInGame = new ConcurrentBag<string>();
        public TschessContext _db;

        public ChessHub(TschessContext db)
        {
            _db = db;
        }

        public override async Task OnConnectedAsync()
        {
            var group = Context.User?.Claims.FirstOrDefault(c => c.Type == "Group")?.Value;

        }

        public async Task EnterWaitingroom()
        {
            if (Context.User?.Identity?.Name is null) { return; }
            ConnectedUsers.Add(Context.User.Identity.Name);
            await Clients.All.SendAsync("SetWaitingroomState", ConnectedUsers);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var username = Context.User?.Identity?.Name;
            if (Context.User?.Identity?.Name is null) { return; }
            ConnectedUsers = new ConcurrentBag<string>(ConnectedUsers.Except(new[] { Context.User?.Identity?.Name }));
            await Clients.All.SendAsync("SetWaitingroomState", ConnectedUsers);
            if(UsersInGame.Contains(Context.User?.Identity?.Name))
            {
                var game = _db.Games.FirstOrDefault(g => g.Player1 == username || g.Player2 == username);
                Resign(game.Guid, username);
            }
            
        }

        public async Task ChallengeUser(string challenged)
        {
            string[] users = { Context.User?.Identity?.Name, challenged };
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
            UsersInGame.Add(challenged);
            UsersInGame.Add(challenger);
        }

        public async Task SetGameState(Guid GameGuid, string fen, string move)
        {
            var game = _db.Games.FirstOrDefault(g => g.Guid == GameGuid);
            if (game is null) return;
            game.GameState = fen;
            try { _db.SaveChanges(); } catch { return; }
            
            string users = game.Player1 + game.Player2;
            if (Context.User?.Identity?.Name == game.Player1) await Clients.Group(users).SendAsync("SetGameState", new string[] { fen, game.Player2, move });
            else if (Context.User?.Identity?.Name == game.Player2) await Clients.Group(users).SendAsync("SetGameState", new string[] { fen, game.Player2, move });

        }

        public async Task GetGameState(Guid GameGuid)
        {
            var game = _db.Games.FirstOrDefault(g => g.Guid == GameGuid);
            if (game is null) return;
            string users = game.Player1 + game.Player2;
            await Clients.Caller.SendAsync("SetGameState", new string[] { game.GameState, game.Player2 });
        }

        public async Task EndGame(Guid GameGuid, string winner, string fen)
        {
            var game = _db.Games.FirstOrDefault(g => g.Guid == GameGuid);
            if (game is null) return;
            string users = game.Player1 + game.Player2;
            game.GameState = fen;
            game.Winner = winner;
            await Clients.Group(users).SendAsync("GameEnd", winner, game.GameState);
            UsersInGame = new ConcurrentBag<string>(ConnectedUsers.Except(new[] { game.Player1, game.Player2}));
        }

        public async Task Resign(Guid GameGuid, string forfeiter)
        {
            var game = _db.Games.FirstOrDefault(g => g.Guid == GameGuid);
            if (game is null) return;
            string users = game.Player1 + game.Player2;
            if (game.Player1 == forfeiter) await Clients.Group(users).SendAsync("Resign", game.Player2, game.GameState);
            if (game.Player2 == forfeiter) await Clients.Group(users).SendAsync("Resign", game.Player1, game.GameState);
            UsersInGame = new ConcurrentBag<string>(ConnectedUsers.Except(new[] { game.Player1, game.Player2 }));
        }

        public async Task<string> GetColor(Guid guid, string username)
        {
            var game = _db.Games.FirstOrDefault(g => g.Guid == guid);
            if (game is null) return "No such game";
            if (game.Player1 == username) return "black";
            else if (game.Player2 == username) return "white";
            return "";
        }
    }
}
