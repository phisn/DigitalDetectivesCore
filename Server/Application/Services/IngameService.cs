using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class IngameService : IIngameService
    {
        public Match Match { get; private set; }

        public void StartMatch(Match match)
        {
            if (Match != null)
                throw new Exception("Match already running");

            Match = match;
        }

        public void CancelMatch()
        {
            Match = null;
            userPlayerMapping.Clear();
        }

        public Player RegisterUser(Guid userID, PlayerColor color)
        {
            Player player = Match.Players.First(p => p.Color == color);

            if (userPlayerMapping.Any(u => player.Id == u.player.Id))
                throw new Exception("Color already occupied");

            userPlayerMapping.Add((userID, player));
            return player;
        }

        public Player RegisterUser(Guid userID)
        {
            Player[] remaining = Match.Players
                .Where(p => !userPlayerMapping.Any(u => u.player.Id == p.Id))
                .ToArray();

            if (remaining.Length == 0)
                throw new Exception("No color remaining");

            Player selected = remaining[new Random().Next(remaining.Length)];
            userPlayerMapping.Add((userID, selected));
            return selected;
        }

        IEnumerable<(Guid userID, Player player)> Registered => userPlayerMapping;

        public void UnregisterUser(Guid userID)
        {
            if (userPlayerMapping.Any(u => u.userID == userID))
                userPlayerMapping.Remove(
                    userPlayerMapping.First(u => u.userID == userID));
        }

        public Guid? UserFromPlayer(PlayerColor color)
            => userPlayerMapping
                .Where(u => u.player.Color == color)
                .Select(u => (Guid?) u.userID)
                .FirstOrDefault();

        public Player PlayerFromUser(Guid userID)
            => userPlayerMapping
                .Where(u => u.userID == userID)
                .Select(u => u.player)
                .FirstOrDefault();

        private List<(Guid userID, Player player)> userPlayerMapping
            = new List<(Guid userID, Player player)>();
    }
}
