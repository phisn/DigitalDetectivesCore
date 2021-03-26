using Microsoft.AspNetCore.SignalR;
using DigitalDetectivesCore.Application.Hubs;
using DigitalDetectivesCore.Application.Hubs.Models;
using DigitalDetectivesCore.Application.Services.Models;
using DigitalDetectivesCore.Game.Models.Match;
using DigitalDetectivesCore.Game.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Services
{
    public class IngameSessionService : IIngameSessionService
    {
        public bool Running { get; private set; }
        public long MatchId { get; private set; }

        public IReadOnlyList<UserPlayerBinding> UsersBindings => users;

        public IngameSessionService(IHubContext<IngameHub, IIngameHubClient> ingameHub)
        {
            this.ingameHub = ingameHub;
        }

        public List<PlayerColor> ColorsUnregistered 
            => players.Where(c => !users.Any(u => u.PlayerId == c.playerId))
                .Select(c => c.color)
                .ToList();

        public async Task UnregisterUser(Guid userId)
        {
            if (users.Any(u => u.UserId == userId))
            {
                UserPlayerBinding user = users.First(u => u.UserId == userId);
                users.Remove(user);

                await ingameHub.Clients.All.PlayerLeftEvent(user.Color);
            }
        }

        public async Task Start(Match match)
        {
            if (Running)
                throw new Exception("Match already running");

            players = match.Players
                .Select(p => (p.Id, p.Color))
                .ToList();
            MatchId = match.Id;
            Running = true;

            await ingameHub.Clients.All.MatchStartedEvent(new MatchInfo
            {
                AvailableColors = ColorsUnregistered,
                Round = match.Round,
                Settings = match.Settings
            });
        }

        public async Task Cancel()
        {
            Running = false;
            users.Clear();

            await ingameHub.Clients.All.MatchCanceldEvent();
        }

        public async Task<PlayerColor> RegisterUser(Guid userId)
        {
            var remaining = players.Where(c => !users.Any(u => u.PlayerId == c.playerId))
                .ToList();

            if (remaining.Count == 0)
                throw new Exception("No color remaining");

            var selected = remaining[new Random().Next(remaining.Count)];

            users.Add(new UserPlayerBinding
            {
                Color = selected.color,
                PlayerId = selected.playerId,
                UserId = userId
            });

            await ingameHub.Clients.All.PlayerJoinedEvent(selected.color);
            return selected.color;
        }

        public async Task<PlayerColor> RegisterUser(Guid userId, PlayerColor color)
        {
            var player = players.First(p => p.color == color);

            if (users.Any(u => player.playerId == u.PlayerId))
                throw new Exception("Color already occupied");

            users.Add(new UserPlayerBinding
            {
                Color = player.color,
                PlayerId = player.playerId,
                UserId = userId
            });

            await ingameHub.Clients.All.PlayerJoinedEvent(color);
            return color;
        }

        private IHubContext<IngameHub, IIngameHubClient> ingameHub;

        private List<UserPlayerBinding> users = new List<UserPlayerBinding>();
        private List<(long playerId, PlayerColor color)> players;
    }
}
