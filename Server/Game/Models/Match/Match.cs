using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Entities
{
    public class Match
    {
        public List<Player> Players { get; private set; }

        public long CurrentPlayerId { get; set; }
        public Player CurrentPlayer { get; private set; }

        public void SelectNextPlayer()
        {
            Player player = Players
                .Where(p => p.Order > CurrentPlayer.Order)
                .Aggregate((p1, p2) => p1.Order < p2.Order ? p1 : p2);

            if (player == null)
            {
                player = FirstPlayer;
            }

            CurrentPlayer = player;
        }

        public Player FirstPlayer
            => Players.Aggregate((p1, p2) => p1.Order < p2.Order ? p1 : p2);
    }
}
