using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class UserIngameService : IUserIngameService
    {
        public UserIngameService(
            IIdentityService identityService,
            IIngameService ingameService,
            IUserPlayerMappingService mappingService)
        {
            this.identityService = identityService;
            this.ingameService = ingameService;
            this.mappingService = mappingService;
        }

        public Player Player 
        {
            get
            {
                if (player == null)
                {
                    if (!mappingService.Registered(identityService.User))
                        throw new Exception("Tried to access non registered player in UserIngameService");

                    player = mappingService.FromUser(identityService.User);
                }

                return player;
            }
        }
        
        public Match Match 
            => ingameService.Match;

        public bool Registered
            => mappingService.Registered(identityService.User);

        public void MakeTurn(
            long targetPosition,
            TicketType ticket,
            bool useDoubleTicket)
        {
            if (player == null && !mappingService.Registered(identityService.User))
                throw new Exception("Tried to make to with non registered player in UserIngameService");

            if (Match.CurrentPlayerId == Player.Id)
            {
                throw new Exception("Player is currently not at turn");
            }

            Match.MakeTurn(
                targetPosition, 
                ticket,
                useDoubleTicket);
        }

        private IIdentityService identityService;
        private IIngameService ingameService;
        private IUserPlayerMappingService mappingService;

        private Player player;
    }
}
