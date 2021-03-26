using DigitalDetectivesCore.Application.Hubs.Models;
using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Hubs.Models
{
    public class DetectiveState : PlayerState
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
