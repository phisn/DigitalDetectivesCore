using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public interface IIngameService
    {
        public Match Match { get; }

        public void StartMatch(Match match);
        public void CancelMatch();

        public Player RegisterUser(Guid userID, PlayerColor color);
        public Player RegisterUser(Guid userID);
        public void UnregisterUser(Guid userID);

        public IEnumerable<(Guid userID, Player player)> Registered { get; }

        public Guid? UserFromPlayer(PlayerColor color);
        public Player PlayerFromUser(Guid userID);
    }
}
