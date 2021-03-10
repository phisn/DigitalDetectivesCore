using Server.Game.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Entities
{
    public class Route : ValueObject
    {
        public static List<Route> RoutesBetween(Station from, Station to)
            => routes[from.Position]
                .Where(r => r.To.Position == to.Position)
                .ToList();

        public static List<Route> RoutesFrom(Station from)
            => routes[from.Position].ToList();

        public Station From { get; }
        public Station To { get; }

        public TicketType Type { get; }

        public Route(TicketType type, long from, long to)
        {
            Type = type;
            From = Station.At(from);
            To = Station.At(to);
        }

        public Route Inverted 
            => new Route(Type, To.Position, From.Position);

        private static ILookup<long, Route> routes = new List<Route>
        {
        }
            .SelectMany(r => new Route[] { r, r.Inverted })
            .Distinct()
            .ToLookup(r => r.From.Position);
    }
}
