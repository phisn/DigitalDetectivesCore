﻿using MediatR;
using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Game.Events
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
