using Microsoft.AspNetCore.SignalR;
using Server.Application.Hubs;
using Server.Application.Hubs.Models;
using Server.Application.Services.Models;
using Server.Game.Models.Match;
using Server.Game.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class IngameService : IIngameService
    {
        public bool Started { get; private set; }

        private List<UserPlayerBinding> users = new List<UserPlayerBinding>();
        public IReadOnlyList<UserPlayerBinding> UsersBindings => users;

        public IngameService(
            IHubContext<IngameHub, IIngameHubClient> ingameHub,
            IMatchRepository matchRepository)
        {
            this.ingameHub = ingameHub;
            this.matchRepository = matchRepository;
        }

        public async Task<Match> GetMatch()
            => await matchRepository.Get(matchId);

        public async Task UnregisterUser(Guid userId)
        {
            if (users.Any(u => u.UserId == userId))
            {
                var user = users.First(u => u.UserId == userId);
                users.Remove(user);

                await ingameHub.Clients.All.PlayerLeftEvent(user.Color);
            }
        }

        public async Task<List<PlayerColor>> GetColorsUnregistered() =>
            (await GetMatch()).Players
                .Where(p => !users.Any(u => u.PlayerId == p.Id))
                .Select(p => p.Color)
                .ToList();

        public async Task StartMatch(Match match)
        {
            if (Started)
                throw new Exception("Match already running");

            await matchRepository.Add(match);
            matchId = match.Id;

            await ingameHub.Clients.All.MatchStartedEvent(new MatchInfo
            {
                AvailableColors = await GetColorsUnregistered(),
                Round = match.Round,
                Settings = match.Settings
            });
        }

        public async Task StartMatch(long matchId)
        {
            if (Started)
                throw new Exception("Match already running");

            await StartMatch(await matchRepository.Get(matchId));
        }

        public async Task CancelMatch()
        {
            Started = false;
            users.Clear();

            await ingameHub.Clients.All.MatchCanceldEvent();
        }

        public async Task<Player> RegisterUser(Guid userId, PlayerColor color)
        {
            Player player = (await GetMatch()).Players.First(p => p.Color == color);

            if (users.Any(u => player.Id == u.PlayerId))
                throw new Exception("Color already occupied");

            users.Add(new UserPlayerBinding
            {
                Color = player.Color,
                PlayerId = player.Id,
                UserId = userId
            });

            await ingameHub.Clients.All.PlayerJoinedEvent(color);
            return player;
        }

        public async Task<Player> RegisterUser(Guid userId)
        {
            Player[] remaining = (await GetMatch()).Players
                .Where(p => !users.Any(u => u.PlayerId == p.Id))
                .ToArray();

            if (remaining.Length == 0)
                throw new Exception("No color remaining");

            Player selected = remaining[new Random().Next(remaining.Length)];
            users.Add(new UserPlayerBinding
            {
                Color = selected.Color,
                PlayerId = selected.Id,
                UserId = userId
            });

            await ingameHub.Clients.All.PlayerJoinedEvent(selected.Color);

            return selected;
        }

        private IHubContext<IngameHub, IIngameHubClient> ingameHub;
        private IMatchRepository matchRepository;

        private long matchId;
    }
}
