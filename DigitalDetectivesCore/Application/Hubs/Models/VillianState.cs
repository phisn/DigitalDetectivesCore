using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Hubs.Models
{
    public class VillianState : PlayerState
    {
        public List<TicketsForPlayer> DetectiveTickets { get; set; }

        public VillianState(
            List<RouteOption> routes,
            long position,
            int villianRevealedIn,
            TicketBag tickets,
            List<TicketsForPlayer> detectiveTickets)
            :
            base(routes,
                 position,
                 villianRevealedIn,
                 tickets)
        {
            DetectiveTickets = detectiveTickets;
        }
    }
}
