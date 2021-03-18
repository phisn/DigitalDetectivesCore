using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Server.Application.Hubs.Models;
using Server.Application.Services;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Hubs
{
    public class GameHub : Hub<IGameHubClient>
    {
        public GameHub(
            ILogger<GameHub> logger,
            IIdentityService identityService)
        {
            this.logger = logger;
            this.identityService = identityService;
        }

        public void TestMethod(int argument)
        {
            logger.LogInformation($"calling: {argument} | {identityService.User.ToString()}");
        }

        public override Task OnConnectedAsync()
        {
            logger.LogInformation($"connecting: {identityService.User.ToString()}");

            TurnDetectiveEvent turn = new TurnDetectiveEvent();

            turn.Routes = new List<ChoosableRoute>
            {
                { new ChoosableRoute(){ Type = TicketType.Yellow, Position = 51 } },
                { new ChoosableRoute(){ Type = TicketType.Yellow, Position = 52 } },
                { new ChoosableRoute(){ Type = TicketType.Yellow, Position = 49 } },
                { new ChoosableRoute(){ Type = TicketType.Yellow, Position = 54 } },

                { new ChoosableRoute(){ Type = TicketType.Green, Position = 52 } },
                { new ChoosableRoute(){ Type = TicketType.Green, Position = 60 } },
                { new ChoosableRoute(){ Type = TicketType.Green, Position = 51 } }
            };

            turn.Tickets = TicketBag.Detective;
            turn.VillianRevealedIn = 3;
            turn.VillianTickets = TicketBag.Villian;
            turn.Position = 50;

            Clients.Caller.OnTurnDetective(turn);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            logger.LogInformation($"disconnected: {identityService.User.ToString()}");
            return base.OnDisconnectedAsync(exception);
        }

        private ILogger<GameHub> logger;
        private IIdentityService identityService;
    }
}
