using MediatR;
using Microsoft.AspNetCore.SignalR;
using DigitalDetectivesCore.Application.Hubs;
using DigitalDetectivesCore.Application.Services;
using DigitalDetectivesCore.Application.Services.Models;
using DigitalDetectivesCore.Game.Events;
using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.DomainEventHandlers
{
    public class MatchTurnGameEventHandler : INotificationHandler<MatchTurnGameEvent>
    {
        public MatchTurnGameEventHandler(
            IHubContext<IngameHub, IIngameHubClient> ingameHub, 
            IIngameSessionService ingameService)
        {
            this.ingameHub = ingameHub;
            this.ingameService = ingameService;
        }

        public async Task Handle(MatchTurnGameEvent notification, CancellationToken cancellationToken)
        {
            foreach (UserPlayerBinding userBinding in ingameService.UsersBindings)
            {
                await ingameHub.Clients.Group(userBinding.UserId.ToString()).UpdateStateByPlayer(
                    notification.Match.Players.First(p => p.Id == userBinding.PlayerId));
            }
        }

        private IHubContext<IngameHub, IIngameHubClient> ingameHub;
        private IIngameSessionService ingameService;

    }
}
