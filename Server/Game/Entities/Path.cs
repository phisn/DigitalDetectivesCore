using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Entities
{
    public class Path
    {
        public Path(TicketType type, long from, long to)
        {
            Type = type;
            From = from;
            To = to;
        }

        public long From { get; private set; }
        public long To { get; private set; }

        public TicketType Type { get; private set; }
    }
}
