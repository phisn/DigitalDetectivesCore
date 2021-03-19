using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public interface IIngameUserService
    {
        public Player Player { get; }
        public Match Match { get; }
        public bool Registered { get; }

        public void MakeTurn(
            long targetPosition,
            TicketType ticket,
            bool useDoubleTicket);
    }
}
