using DigitalDetectivesCore.Game.Models.Match;
using DigitalDetectivesCore.Game.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Infrastructure.Repositories
{
    public class TestMatchRepository : IMatchRepository
    {
        static TestMatchRepository()
        {
            int id = 1;

            match = new Match(MatchSettings.Default);
            match.GetType().GetProperty("Id").SetValue(match, id);

            foreach (Player player in match.Players)
            {
                player.GetType().GetProperty("Id").SetValue(player, ++id);
            }

            match.GetType().GetProperty("CurrentPlayerId").SetValue(match, match.CurrentPlayer.Id);
        }

        public TestMatchRepository(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Add(Match match)
        {
            throw new NotImplementedException();
        }

        public Task<Match> Get(long matchId)
        {
            if (matchId != match.Id)
            {
                throw new ArgumentException("Match not found");
            }

            return Task.FromResult(match);
        }

        public Task<Match> LastMatch()
        {
            return Task.FromResult(match);
        }

        public Task Save()
        {
            foreach (INotification notification in match.GameEvents)
            {
                mediator.Publish(notification);
            }

            match.ClearEvents();

            return Task.CompletedTask;
        }

        private IMediator mediator;
        static private Match match;
    }
}
