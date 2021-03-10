using Server.Game.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Entities
{
    public class Station : ValueObject
    {
        public static Station At(long position)
            => stations[position];

        public TicketType Type { get; }
        public long Position { get; }

        private Station(TicketType type, long position)
        {
            Type = type;
            Position = position;
        }

        private static SortedList<long, Station> stations 
            = new SortedList<long, Station>
        {
        };
    }
}
