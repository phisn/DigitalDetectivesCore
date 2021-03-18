using Iot.Device.BrickPi3.Sensors;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class UserPlayerMappingService : IUserPlayerMappingService
    {
        public UserPlayerMappingService(IIngameService ingameService)
        {
            this.ingameService = ingameService;
        }

        public void Clear()
            => players.Clear();

        public Guid FromPlayer(PlayerColor color)
            => players.First(u => u.Color == color).UserID;

        public Player FromUser(Guid userID)
            => PlayerFromID(players.First(u => u.UserID == userID).PlayerID);

        public Player Register(Guid userID, PlayerColor color)
        {
            Player player = ingameService.Match.Players.First(p => p.Color == color);

            if (players.Any(u => player.Id == u.PlayerID))
                throw new Exception("Color already occupied");
            
            players.Add((userID, color, player.Id));
            return player;
        }

        public Player Register(Guid userID)
        {
            Player[] remaining = ingameService.Match.Players
                .Where(p => !players.Any(u => u.PlayerID == p.Id))
                .ToArray();

            if (remaining.Length == 0)
                throw new Exception("No color remaining");

            Player selected = remaining[new Random().Next(remaining.Length)];
            players.Add((userID, selected.Color, selected.Id));
            return selected;
        }

        public void Unregister(Guid userID)
            => players.Remove(players.First(u => u.UserID == userID));

        public bool Registered(Guid userID)
            => players.Any(u => u.UserID == userID);

        public bool Registered(PlayerColor color)
            => players.Any(u => u.Color == color);

        private IIngameService ingameService;
        private List<(Guid UserID, PlayerColor Color, long PlayerID)> players;

        private Player PlayerFromID(long playerID)
            => ingameService.Match.Players.First(p => p.Id == playerID);
    }
}
