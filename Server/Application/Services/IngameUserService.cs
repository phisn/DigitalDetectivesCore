using Server.Application.Services.Models;
using Server.Game.Models.Match;
using Server.Game.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class IngameUserService : IIngameUserService
    {
        public IngameUserService(
            IMatchRepository matchRepository,
            IIdentityService identityService,
            IIngameService ingameService)
        {
            this.matchRepository = matchRepository;
            this.identityService = identityService;
            this.ingameService = ingameService;
        }

        public bool Registered
            => ingameService.UsersBindings.Any(u => u.UserId == identityService.User);

        public UserPlayerBinding Binding
            => ingameService.UsersBindings.First(u => u.UserId == identityService.User);

        public async Task<Player> GetPlayer()
        {
            if (!Registered)
                throw new Exception("Tried to access non registered player in UserIngameService");

            Match match = await ingameService.GetMatch();
            long playerId = Binding.PlayerId;

            return match.Players.First(p => p.Id == playerId);
        }

        public async Task MakeTurn(
            long targetPosition,
            TicketType ticket,
            bool useDoubleTicket)
        {
            if (!ingameService.Started)
            {
                throw new Exception("Match currently not running");
            }

            Match match = await ingameService.GetMatch();

            if (match.CurrentPlayerId == Binding.PlayerId)
            {
                throw new Exception("Player is currently not at turn");
            }

            match.MakeTurn(
                targetPosition, 
                ticket,
                useDoubleTicket);

            await matchRepository.Save();
        }

        private IMatchRepository matchRepository;
        private IIdentityService identityService;
        private IIngameService ingameService;
    }
}
