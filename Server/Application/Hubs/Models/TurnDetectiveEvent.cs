using Server.Application.Hubs.Models;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Hubs.Models
{
    public class TurnDetectiveEvent : IngameEvent
    {
        public TicketBag VillianTickets { get; set; }
        public List<ChoosableRoute> Routes { get; set; }
    }
}
