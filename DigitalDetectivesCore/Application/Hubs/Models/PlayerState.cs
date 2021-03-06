﻿using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Hubs.Models
{
    public class PlayerState
    {
        // empty if currently not at turn
        public List<RouteOption> Routes { get; set; }

        public long Position { get; set; }
        public int VillianRevealedIn { get; set; }
        public TicketBag Tickets { get; set; }

        public PlayerState(
            List<RouteOption> routes, 
            long position, 
            int villianRevealedIn, 
            TicketBag tickets)
        {
            Routes = routes;
            Position = position;
            VillianRevealedIn = villianRevealedIn;
            Tickets = tickets;
        }
    }
}
