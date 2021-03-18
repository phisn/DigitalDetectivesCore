using Server.Game.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Models.Match
{
    public class Player : Entity
    {
        public static Player Villian(
            long position,
            int order,
            TicketBag tickets)
        {
            return new Player(
                position,
                order,
                PlayerRole.Villian,
                PlayerColor.Villian)
            {
                Tickets = tickets
            };
        }

        public static Player Detective(
            PlayerColor color,
            long position,
            int order,
            TicketBag tickets)
        {
            return new Player(
                position,
                order,
                PlayerRole.Villian,
                color)
            {
                Tickets = tickets,
            };
        }

        public override long Id { get; protected set; }

        public int Order { get; private set; }
        
        public TicketBag Tickets { get; private set; }
        public PlayerRole Role { get; private set; }
        public PlayerColor Color { get; private set; }
        public Station Initial { get; private set; }

        private List<Route> path { get; set; }
        public IReadOnlyCollection<Route> Path => path;

        public long MatchId { get; private set; }
        public Match Match { get; private set; }

        public Station Position() =>
            Models.Match.Station.At(path[^1].To.Position);

        public Route RouteWith(long target, TicketType type)
            => Route.RoutesBetween(Position(), Models.Match.Station.At(target))
                .FirstOrDefault(r => r.Type == type);

        public List<Route> ValidRoutes()
            => Route.RoutesFrom(Position())
                .Where(r => IsValidRoute(r))
                .ToList();

        public bool HasAnyTicket(TicketType type)
            => type switch
            {
                TicketType.Yellow => Tickets.Yellow > 0,
                TicketType.Green => Tickets.Green > 0,
                TicketType.Red => Tickets.Red > 0,
                TicketType.Black => Tickets.Black > 0,
                _ => throw new NotImplementedException()
            };

        public void MakeTurn(
            long targetPosition,
            TicketType ticket,
            bool useDoubleTicket)
        {
            if (!HasAnyTicket(ticket))
            {
                throw new ArgumentException($"Ticket '{ticket.ToString()}' not available anymore");
            }

            if (useDoubleTicket && Tickets.Double == 0)
            {
                throw new ArgumentException("Double ticket not available");
            }

            Route route = RouteWith(targetPosition, ticket);

            if (route == null)
            {
                throw new ArgumentException("Invalid target position");
            }

            if (!IsValidRoute(route))
            {
                throw new ArgumentException("Invalid route selected");
            }

            Tickets.RemoveTicket(ticket);
            path.Add(route);

            if (useDoubleTicket)
            {
                Tickets.RemoveDoubleTicket();
            }
        }

        private bool IsValidRoute(Route route)
            => HasAnyTicket(route.Type) &&
            // neither detectives nor the villian should 
            // be able to walk on detectives
                !Match.Detectives.Any(d => d.Position() == route.To);

        private Player(
            long initialPosition,
            int order,
            PlayerRole role,
            PlayerColor color)
        {
            Initial = Station.At(initialPosition);
            Role = role;
            Color = color;
        }
    }
}
