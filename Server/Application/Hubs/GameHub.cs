using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Hubs
{
    public class GameHub : Hub<IGameHubClient>
    {
        public override Task OnConnectedAsync()
        {
            Clients.Caller.OnTurn();

            return base.OnConnectedAsync();
        }
    }
}
