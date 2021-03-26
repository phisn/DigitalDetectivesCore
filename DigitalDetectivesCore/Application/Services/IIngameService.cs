using DigitalDetectivesCore.Application.Services.Models;
using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Services
{
    public interface IIngameService
    {
        public bool Registered { get; }
        public UserPlayerBinding Binding { get; }

        public Task<Match> GetMatch();
        public Task<Player> GetPlayer();

        public Task MakeTurn(
            long targetPosition,
            TicketType ticket,
            bool useDoubleTicket);
    }
}
