using Server.Game.Events;
using Server.Game.SeedWork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Models.Match
{
    public class Match : Entity
    {
        public Match(MatchSettings settings)
        {
            if (settings.Valid)
            {
                throw new ArgumentException("Got invalid settings");
            }

            MatchState = MatchState.Running;
            Round = 0;
            Settings = settings;

            Random random = new Random();

            foreach ((int index, int player) in Enumerable
                .Range(1, settings.PlayerCount - 1)
                .OrderBy(_ => random.Next())
                .Select((k, p) => (k, p)))
            {
                players.Add(Player.Detective(
                    (PlayerColor) player,
                    Station.RandomInitial.Position,
                    index,
                    settings.DetectiveTickets));
            }

            players.Add(CurrentPlayer = Player.Villian(
                Station.RandomInitial.Position,
                0,
                settings.VillianTickets));
        }

        public override long Id { get; protected set; }

        public int Round { get; set; }
        public MatchSettings Settings { get; private set; }
        public MatchState MatchState { get; private set; }

        private long? lastStationVillianReveal { get; set; }
        public Station LastStationVillianReveal => lastStationVillianReveal == null
            ? null
            : Station.At(lastStationVillianReveal.Value);

        public bool IsVillianRevealRound
            => (Round + Settings.ShowVillianEvery - Settings.ShowVillianAfter)
                    % Settings.ShowVillianEvery == 0;

        public List<Player> players { get; private set; }
        public IReadOnlyCollection<Player> Players => players;

        public long CurrentPlayerId { get; private set; }
        public Player CurrentPlayer { get; private set; }

        public void MakeTurn(
            long targetPosition,
            TicketType ticket,
            bool useDoubleTicket = false)
        {
            if (MatchState != MatchState.Running)
            {
                throw new Exception("Invalid state");
            }

            CurrentPlayer.MakeTurn(
                targetPosition,
                ticket,
                useDoubleTicket);

            if (CurrentPlayer.Role == PlayerRole.Detective)
            {
                Villian.Tickets.AddTicket(ticket);

                if (CurrentPlayer.Station == Villian.Station)
                {
                    MatchState = MatchState.DetectivesWon;
                    AddGameEvent(new MatchOverGameEvent(this));
                }
            }
            else
            {
                // it should not be possible to move onto a detective
                Debug.Assert(!Detectives.Any(p => p.Station == CurrentPlayer.Station));
                
                ++Round;
                AddGameEvent(new MatchRoundOverGameEvent());

                if (IsVillianRevealRound)
                {
                    lastStationVillianReveal = Villian.Station.Position;
                    AddGameEvent(new MatchVillianRevealedGameEvent());
                }

                if (Round >= Settings.Rounds)
                {
                    MatchState = MatchState.VillianWon;
                    AddGameEvent(new MatchOverGameEvent(this));
                }
            }

            if (!useDoubleTicket)
            {
                Player player = Players
                    .OrderBy(p => p.Order)
                    .FirstOrDefault(p => CurrentPlayer.Order < p.Order);

                if (player == null)
                {
                    player = FirstPlayer;
                }

                CurrentPlayer = player;
            }

            // ensure next player has any valid moves
            if (CurrentPlayer.ValidRoutes.Count == 0)
            {
                MatchState = CurrentPlayer.Role == PlayerRole.Detective
                    ? MatchState.VillianWon
                    : MatchState.DetectivesWon;
                AddGameEvent(new MatchOverGameEvent(this));
            }

            AddGameEvent(new MatchTurnOverGameEvent());
        }

        public List<Player> Detectives
            => players.Where(p => p.Role == PlayerRole.Detective).ToList();
        public Player Villian
            => Players.First(p => p.Role == PlayerRole.Villian);
        public Player FirstPlayer
            => Players.Aggregate((p1, p2) => p1.Order < p2.Order ? p1 : p2);
    }
}
