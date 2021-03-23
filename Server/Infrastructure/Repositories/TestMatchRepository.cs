using Server.Game.Models.Match;
using Server.Game.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories
{
    public class TestMatchRepository : IMatchRepository
    {
        public Task Add(Match match)
        {
            throw new NotImplementedException();
        }

        public Task<Match> Get(long matchId)
        {
            if (matchId != match.Id)
            {
                throw new ArgumentException("Match not found");
            }

            return Task.FromResult(match);
        }

        public Task<Match> LastMatch()
        {
            return Task.FromResult(match);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }

        private Match match;
    }
}
