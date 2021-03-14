using Server.Application.Hubs.Models;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Models.Bindings
{
    public class TurnVillianEvent : ITurnEvent
    {
        public int VillianRevealedIn { get; set; }
        public TicketBag Tickets { get; set; }
        public List<TicketBag> OtherPlayersTickets { get; set; }
        public List<Route> Routes { get; set; }
    }
}
