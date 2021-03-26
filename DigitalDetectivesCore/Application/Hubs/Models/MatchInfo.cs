using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Hubs.Models
{
    public class MatchInfo
    {
        public List<PlayerColor> AvailableColors { get; set; }
        public int Round { get; set; }
        public MatchSettings Settings { get; set; }
    }
}
