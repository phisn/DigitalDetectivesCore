using Server.Application.Services.Models;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public interface IIngameUserService
    {
        public bool Registered { get; }
        public UserPlayerBinding Binding { get; }

        public Task<Player> GetPlayer();
        public Task MakeTurn(
            long targetPosition,
            TicketType ticket,
            bool useDoubleTicket);
    }
}
