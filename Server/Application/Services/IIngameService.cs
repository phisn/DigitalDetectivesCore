using Server.Application.Services.Models;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public interface IIngameService
    {
        public bool Started { get; }
        public IReadOnlyList<UserPlayerBinding> UsersBindings { get; }
        
        public Task<Match> GetMatch();
        public Task<List<PlayerColor>> GetColorsUnregistered();
        
        public Task StartMatch(Match match);
        public Task StartMatch(long matchId);
        public Task CancelMatch();

        public Task<Player> RegisterUser(Guid userID, PlayerColor color);
        public Task<Player> RegisterUser(Guid userID);
        public Task UnregisterUser(Guid userID);
    }
}
