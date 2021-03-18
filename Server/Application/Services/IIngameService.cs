using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public interface IIngameService
    {
        public Match Match { get; }

        public void StartMatch(Match match);
        public void CancelMatch();
    }
}
