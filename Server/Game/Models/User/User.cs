using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<Player> Players { get; set; }

        public List<Match> Matches
            => Players
                .Select(p => p.Match)
                .ToList();
    }
}
