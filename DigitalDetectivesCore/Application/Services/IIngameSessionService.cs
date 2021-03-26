using DigitalDetectivesCore.Application.Services.Models;
using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Services
{
    public interface IIngameSessionService
    {
        public bool Running { get; }
        public long MatchId { get; }

        public IReadOnlyList<UserPlayerBinding> UsersBindings { get; }
        public List<PlayerColor> ColorsUnregistered { get; }
        
        public Task Start(Match match);
        public Task Cancel();

        public Task<PlayerColor> RegisterUser(Guid userID);
        public Task<PlayerColor> RegisterUser(Guid userID, PlayerColor color);
        public Task UnregisterUser(Guid userID);
    }
}
