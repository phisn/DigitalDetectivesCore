using MediatR;
using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Game.Events
{
    public class MatchOverGameEvent : INotification
    {
        public Match Match { get; }

        public MatchOverGameEvent(Match match)
        {
            Match = match;
        }
    }
}
