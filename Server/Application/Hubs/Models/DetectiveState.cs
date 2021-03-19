using Server.Application.Hubs.Models;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Hubs.Models
{
    public class DetectiveState : PlayerInfo
    {
        public TicketBag VillianTickets { get; set; }

        public DetectiveState(
            List<RouteOption> routes,
            long position,
            int villianRevealedIn,
            TicketBag tickets,
            TicketBag villianTickets)
            : 
            base(routes,
                 position,
                 villianRevealedIn,
                 tickets)
        {
            VillianTickets = villianTickets;
        }
    }
}
