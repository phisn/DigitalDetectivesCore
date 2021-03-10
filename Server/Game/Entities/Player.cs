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

        public TicketBag Tickets { get; private set; }

        public List<long> Path { get; private set; }
        public long Position => Path.First();

        public RoleType Role { get; private set; }

        public User User { get; set; }

        public long MatchId { get; private set; }
        public Match Match { get; set; }

        public bool HasAnyTicket(TicketBag.TicketType type)
            => type switch
            {
                Tickets.TicketType.Yellow => Tickets.Yellow > 0,
                Tickets.TicketType.Green => Tickets.Green > 0,
                Tickets.TicketType.Red => Tickets.Red > 0,
                Tickets.TicketType.Black => Tickets.Black > 0,
                Tickets.TicketType.Double => Tickets.Double > 0,
                _ => throw new NotImplementedException()
            };

        private Player(
            User user,
            int initialPosition,
            RoleType role)
        {
            Path.Add(initialPosition);

            Id = user.Id;
            User = user ?? throw new ArgumentException();
            Role = role;
        }
    }
}
