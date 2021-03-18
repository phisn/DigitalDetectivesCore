using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Hubs.Models
{
    public class IngameEvent
    {
        public long Position { get; set; }
        public int VillianRevealedIn { get; set; }
        public TicketBag Tickets { get; set; }
    }
}
