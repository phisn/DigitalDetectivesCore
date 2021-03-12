using Server.Game.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Models.Match
{
    public class Station : ValueObject
    {
        public static Station At(long position)
            => stations[position];

        public static Station RandomInitial
            => At(initial[new Random().Next(initial.Count)]);

        public TicketType Type { get; }
        public long Position { get; }

        public override bool Equals(object obj)
        {
            return obj is Station station &&
                   Position == station.Position;
        }

        private Station(TicketType type, long position)
        {
            Type = type;
            Position = position;
        }

        private static SortedList<long, Station> stations 
            = new SortedList<long, Station>
        {
        };

        private static List<long> initial = new List<long>
        {

        };
    }
}
