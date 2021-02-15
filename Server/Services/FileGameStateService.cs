using Server.Game;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class FileGameStateService : IGameStateService
    {
        public Phase CurrentPhase => currentPhase;

        private Phase currentPhase = Phase.Collect;
    }
}
