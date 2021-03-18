using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class IngameService : IIngameService
    {
        public Match Match { get; private set; }

        public void StartMatch(Match match)
        {
            if (Match != null)
                throw new Exception("Match already running");

            Match = match;
        }

        public void CancelMatch()
        {
            Match = null;
        }
    }
}
