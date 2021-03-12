using MediatR;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Events
{
    public class MatchOverDomainEvent : INotification
    {
        public Match Match { get; }

        public MatchOverDomainEvent(Match match)
        {
            Match = match;
        }
    }
}
