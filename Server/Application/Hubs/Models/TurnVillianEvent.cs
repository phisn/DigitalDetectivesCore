﻿using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Hubs.Models
{
    public class TurnVillianEvent : IngameEvent
    {
        public List<TicketBagForPlayer> DetectiveTickets { get; set; }
        public List<ChoosableRoute> Routes { get; set; }
    }
}
