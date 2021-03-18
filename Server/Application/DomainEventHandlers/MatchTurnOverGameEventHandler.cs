using MediatR;
using Server.Application.Hubs;
using Server.Game.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.DomainEventHandlers
{
    public class MatchTurnOverGameEventHandler : INotificationHandler<MatchTurnOverGameEvent>
    {
        public MatchTurnOverGameEventHandler(IngameHub gameHub)
        {
            this.gameHub = gameHub;
        }

        public async Task Handle(MatchTurnOverGameEvent notification, CancellationToken cancellationToken)
        {
        }

        private IngameHub gameHub;
    }
}
