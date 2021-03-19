using MediatR;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Events
{
    public class MatchTurnGameEvent : INotification
    {
        public Match Match { get; set; }

        public MatchTurnGameEvent(Match match)
        {
            Match = match;
        }
    }
}
