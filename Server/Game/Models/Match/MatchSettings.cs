using Server.Game.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Models.Match
{
    public class MatchSettings : ValueObject
    {
        public int Rounds { get; private set; }
        public int ShowVillianAfter { get; private set; }
        public int ShowVillianEvery { get; private set; }

        public TicketBag DetectiveTickets { get; private set; }
        public TicketBag VillianTickets { get; private set; }

        public int VillianBlackTicketMulti { get; set; }
    }
}
