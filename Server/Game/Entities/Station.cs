using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Entities
{
    public class Station
    {
        public Station(TicketType type, long position)
        {
            Type = type;
            Position = position;
        }

        public TicketType Type { get; private set; }
        public long Position { get; private set; }

        private static Station[] Stations =
        {
            new Station(TicketType.Red, 1),
        };
    }
}
