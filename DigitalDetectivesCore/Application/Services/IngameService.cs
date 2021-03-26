using DigitalDetectivesCore.Application.Services.Models;
using DigitalDetectivesCore.Game.Models.Match;
using DigitalDetectivesCore.Game.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Services
{
    public class IngameService : IIngameService
    {
        public IngameService(
            IMatchRepository matchRepository,
            IIdentityService identityService,
            IIngameSessionService ingameService)
        {
            this.matchRepository = matchRepository;
            this.identityService = identityService;
            this.ingameService = ingameService;
        }

        public bool Registered
            => ingameService.UsersBindings.Any(u => u.UserId == identityService.User);

        public UserPlayerBinding Binding
            => ingameService.UsersBindings.First(u => u.UserId == identityService.User);

        public async Task<Match> GetMatch()
        {
            if (!ingameService.Running)
                throw new Exception("Unable to get non running match");

            return await matchRepository.Get(ingameService.MatchId);
        }

        public async Task<Player> GetPlayer()
        {
            if (!Registered)
                throw new Exception("Tried to access non registered player in UserIngameService");

            long playerId = Binding.PlayerId;
            Match match = await GetMatch();

            return match.Players.First(p => p.Id == playerId);
        }

        public async Task MakeTurn(
            long targetPosition,
            TicketType ticket,
            bool useDoubleTicket)
        {
            if (!ingameService.Running)
            {
                throw new Exception("Match currently not running");
            }

            Match match = await GetMatch();

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
        private IIngameSessionService ingameService;
    }
}
