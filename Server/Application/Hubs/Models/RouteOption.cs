using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Hubs.Models
{
    public class RouteOption
    {
        public long Position { get; set; }
        public TicketType Type { get; set; }
    }
}
