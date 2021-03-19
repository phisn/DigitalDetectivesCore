using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class IngameUserService : IIngameUserService
    {
        public IngameUserService(
            IIdentityService identityService,
            IIngameService ingameService)
        {
            this.identityService = identityService;
            this.ingameService = ingameService;
        }

        public Player Player 
        {
            get
            {
                if (player == null)
                    player = ingameService.PlayerFromUser(identityService.User);

                if (player == null)
                    throw new Exception("Tried to access non registered player in UserIngameService");

                return player;
            }
        }
        
        public Match Match 
            => ingameService.Match;

        public bool Registered
            => ingameService.PlayerFromUser(identityService.User) == null;

        public void MakeTurn(
            long targetPosition,
            TicketType ticket,
            bool useDoubleTicket)
        {
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
        
        private Player player;
    }
}
