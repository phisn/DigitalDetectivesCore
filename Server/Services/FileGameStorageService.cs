using Server.Game;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class FileGameStorageService : IGameStorageService
    {
        public State CurrentState => currentPhase;

        private State currentPhase = State.Collect;
    }
}
