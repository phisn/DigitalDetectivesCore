using MediatR;
using Microsoft.AspNetCore.SignalR;
using Server.Application.Hubs;
using Server.Application.Services;
using Server.Application.Services.Models;
using Server.Game.Events;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.DomainEventHandlers
{
    public class MatchTurnGameEventHandler : INotificationHandler<MatchTurnGameEvent>
    {
        public MatchTurnGameEventHandler(
            IHubContext<IngameHub, IIngameHubClient> ingameHub, 
            IIngameService ingameService)
        {
            this.ingameHub = ingameHub;
            this.ingameService = ingameService;
        }

        public async Task Handle(MatchTurnGameEvent notification, CancellationToken cancellationToken)
        {
            Match match = await ingameService.GetMatch();

            foreach (UserPlayerBinding userBinding in ingameService.UsersBindings)
            {
                await ingameHub.Clients.Group(userBinding.UserId.ToString()).UpdateStateByPlayer(
                    match.Players.First(p => p.Id == userBinding.PlayerId));
            }
        }

        private IHubContext<IngameHub, IIngameHubClient> ingameHub;
        private IIngameService ingameService;

    }
}
