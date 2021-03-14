using MediatR;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Events
{
    public class MatchTurnOverGameEvent : INotification
    {
        public Match Match { get; set; }

        public MatchTurnOverGameEvent(Match match)
        {
            Match = match;
        }
    }
}
