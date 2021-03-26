using MediatR;
using DigitalDetectivesCore.Application.Hubs;
using DigitalDetectivesCore.Application.Services;
using DigitalDetectivesCore.Game.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.DomainEventHandlers
{
    public class MatchOverDomainEventHandler
        : INotificationHandler<MatchOverGameEvent>
    {
        public MatchOverDomainEventHandler(
            IngameHub gameHub)
        {
            this.gameHub = gameHub;
        }

        public async Task Handle(MatchOverGameEvent notification, CancellationToken cancellationToken)
        {
        }

        private IngameHub gameHub;
    }
}
