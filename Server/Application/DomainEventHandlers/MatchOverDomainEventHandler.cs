using MediatR;
using Server.Application.Services;
using Server.Game.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.DomainEventHandlers
{
    public class MatchOverDomainEventHandler
        : INotificationHandler<MatchOverGameEvent>
    {
        public MatchOverDomainEventHandler(
            GameStateService stateService)
        {
            this.stateService = stateService;
        }

        public async Task Handle(MatchOverGameEvent notification, CancellationToken cancellationToken)
        {

        }

        private GameStateService stateService;
    }
}
