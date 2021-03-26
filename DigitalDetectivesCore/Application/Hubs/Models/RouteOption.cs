using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Hubs.Models
{
    public class RouteOption
    {
        public long Position { get; set; }
        public TicketType Type { get; set; }
    }
}
