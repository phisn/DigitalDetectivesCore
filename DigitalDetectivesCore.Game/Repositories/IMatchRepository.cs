using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Game.Repositories
{
    public interface IMatchRepository
    {
        Task<Match> LastMatch();

        Task Add(Match match);
        Task<Match> Get(long matchId);

        Task Save();
    }
}
