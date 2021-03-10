using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Entities
{
    public class Player
    {
        public static Player Villian(
            User user,
            int position,
            int blackTicket)
        {
            return Villian(
                user,
                position,
                TicketBag.Villian(blackTicket));
        }

        public static Player Villian(
            User user,
            int position,
            TicketBag tickets)
        {
            Player player = new Player(
                user,
                position,
                RoleType.Villian);

            player.Tickets = tickets;

            return player;
        }

        [Key, ForeignKey("User")]
        public long Id { get; private set; }

        public int Order { get; private set; }

        public TicketBag Tickets { get; private set; }
        public RoleType Role { get; private set; }
        public List<Station> Path { get; private set; }

        public User User { get; set; }

        public long MatchId { get; private set; }
        public Match Match { get; set; }


        public Station CurrentStation => Path.First();

        public bool HasAnyTicket(TicketType type)
            => type switch
            {
                TicketType.Yellow => Tickets.Yellow > 0,
                TicketType.Green => Tickets.Green > 0,
                TicketType.Red => Tickets.Red > 0,
                TicketType.Black => Tickets.Black > 0,
                TicketType.Double => Tickets.Double > 0,
                _ => throw new NotImplementedException()
            };

        public void MakeTurn(
            TicketType ticket,
            long targetPosition)
        {
            if (Match.CurrentPlayer != this)
            {
                throw new Exception("Invalid turn");
            }

            if (HasAnyTicket(ticket))
            {
                throw new ArgumentException("Missing ticket");
            }

            Route route = Route.RoutesFrom(Station.At(CurrentStation.Position))
                .FirstOrDefault(r =>
                    r.To.Position == targetPosition &&
                    r.Type == ticket);

            if (route == null)
            {
                throw new ArgumentException("Invalid target position");
            }

            Path.Add(Station.At(targetPosition));
            Match.SelectNextPlayer();
        }

        private Player(
            User user,
            int initialPosition,
            RoleType role)
        {
            Path.Add(Station.At(initialPosition));

            Id = user.Id;
            User = user ?? throw new ArgumentException();
            Role = role;
        }
    }
}
