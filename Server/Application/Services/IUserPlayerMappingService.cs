using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public interface IUserPlayerMappingService
    {
        public void Clear();

        public Player Register(Guid userID, PlayerColor color);
        public Player Register(Guid userID);
        public void Unregister(Guid userID);

        public Player FromUser(Guid userID);
        public Guid FromPlayer(PlayerColor color);

        public bool Registered(Guid userID);
        public bool Registered(PlayerColor color);
    }
}
