using DigitalDetectivesCore.Game.Models.Match;
using DigitalDetectivesCore.Game.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        public Task Add(Match match)
        {
            throw new NotImplementedException();
        }

        public Task<Match> Get(long matchId)
        {
            throw new NotImplementedException();
        }

        public Task<Match> LastMatch()
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
}
