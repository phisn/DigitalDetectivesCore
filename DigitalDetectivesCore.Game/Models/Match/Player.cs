using DigitalDetectivesCore.Game.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Game.Models.Match
{
    public class Player : Entity
    {
        public Player(
            Match parent,
            long position,
            int order,
            PlayerColor color,
            TicketBag tickets)
        {
            MatchId = parent.Id;
            Match = parent;

            Initial = Station.At(position);
            Order = order;
            Color = color;
            Tickets = tickets;
            Role = color == PlayerColor.Villian
                ? PlayerRole.Villian
                : PlayerRole.Detective;
        }

        public override long Id { get; protected set; }

        public int Order { get; private set; }
        
        public TicketBag Tickets { get; private set; }
        public PlayerRole Role { get; private set; }
        public PlayerColor Color { get; private set; }
        public Station Initial { get; private set; }

        private List<Route> path { get; set; } = new List<Route>();
        public IReadOnlyCollection<Route> Path => path;

        public long MatchId { get; private set; }
        public Match Match { get; private set; }

        public Station Position() => path.Count == 0
            ? Initial
            : Station.At(path[^1].To.Position);

        public Route RouteWith(long target, TicketType type)
            => Route.RoutesBetween(Position(), Station.At(target))
                .FirstOrDefault(r => r.Type == type);

        public List<Route> ValidRoutes()
            => Route.RoutesFrom(Position())
                .Where(r => IsValidRoute(r))
                .ToList();

        public bool IsCurrentPlayer()
            => Id == Match.CurrentPlayer.Id;

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
                throw new DomainException($"Ticket '{ticket.ToString()}' not available anymore");
            }

            if (useDoubleTicket && Tickets.Double == 0)
            {
                throw new DomainException("Double ticket not available");
            }

            Route route = RouteWith(targetPosition, ticket);

            if (route == null)
            {
                throw new DomainException("Invalid target position");
            }

            if (!IsValidRoute(route))
            {
                throw new DomainException("Invalid route selected");
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
    }
}
