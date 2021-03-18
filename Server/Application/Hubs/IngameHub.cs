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
    public class IngameHub : Hub<IIngameHubClient>
    {
        public IngameHub(
            ILogger<IngameHub> logger,
            IIdentityService identityService,
            UserIngameService userIngameService)
        {
            this.logger = logger;
            this.identityService = identityService;
            this.userIngameService = userIngameService;
        }

        public void MakeTurn(TicketType ticket, long position)
        {
        }

        public override Task OnConnectedAsync()
        {
            if (!userIngameService.Registered)
            {
                Context.Abort();
            }

            if (userIngameService.Player.Id == userIngameService.Match.CurrentPlayerId)
            {
                SendCurrentPlayerTurnEvent();
            }
            else
            {
                // send not current player turn
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            logger.LogInformation($"disconnected: {identityService.User.ToString()}");
            return base.OnDisconnectedAsync(exception);
        }

        private ILogger<IngameHub> logger;
        private IIdentityService identityService;
        private UserIngameService userIngameService;
        
        private void SendCurrentPlayerTurnEvent()
        {
            if (userIngameService.Player.Role == PlayerRole.Detective)
            {
                TurnDetectiveEvent turnEvent = new TurnDetectiveEvent
                {
                    Position = userIngameService.Player.Position().Position,
                    Routes = userIngameService.Player.ValidRoutes()
                        .Select(r => new ChoosableRoute
                        {
                            Position = r.To.Position,
                            Type = r.Type
                        })
                        .ToList(),
                    Tickets = userIngameService.Player.Tickets,
                    VillianRevealedIn = userIngameService.Match.VillianRevealedIn(),
                    VillianTickets = userIngameService.Match.Villian.Tickets
                };
                Clients.Caller.OnTurnDetective(turnEvent);
            }
            else
            {
                TurnVillianEvent turnEvent = new TurnVillianEvent
                {
                    Position = userIngameService.Player.Position().Position,
                    Routes = userIngameService.Player.ValidRoutes()
                        .Select(r => new ChoosableRoute
                        {
                            Position = r.To.Position,
                            Type = r.Type
                        })
                        .ToList(),
                    Tickets = userIngameService.Player.Tickets,
                    VillianRevealedIn = userIngameService.Match.VillianRevealedIn(),
                    DetectiveTickets = userIngameService.Match.Detectives
                        .Select(p => new TicketBagForPlayer
                        {
                            Color = p.Color,
                            Tickets = p.Tickets
                        })
                        .ToList()
                };
                Clients.Caller.OnTurnVillian(turnEvent);
            }
        }
    }
}
