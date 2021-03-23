﻿using Server.Game.Models.Match;
using Server.Game.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        public Task Add(Match match)
        {
            throw new NotImplementedException();
        }

        public Task<Match> Get(long matchId)
        {
            // assuming the same match will often be 
            // accessed repeatedly
            if (matchCache != null && matchCache.Id == matchId)
            {
                return Task.FromResult(matchCache);
            }


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

        private Match matchCache;
    }
}
