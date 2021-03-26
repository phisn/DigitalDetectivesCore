using DigitalDetectivesCore.Game.Events;
using DigitalDetectivesCore.Game.SeedWork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Game.Models.Match
{
    /*
     * Move from properties to functions
     */
    public class Match : Entity
    {
        public Match(MatchSettings settings)
        {
            if (!settings.Valid())
            {
                throw new DomainException("Got invalid settings");
            }

            MatchState = MatchState.Running;
            Round = 0;
            Settings = settings;

            Random random = new Random();
            List<Station> initial = Station.RandomInitial(settings.PlayerCount);

            foreach ((int player, int index) in Enumerable
                .Range(1, settings.PlayerCount - 1)
                .OrderBy(_ => random.Next())
                .Select((p, k) => (p, k)))
            {
                players.Add(new Player(
                    this,
                    initial[index].Position,
                    index,
                    (PlayerColor) player,
                    settings.DetectiveTickets));
            }

            players.Add(CurrentPlayer = new Player(
                this,
                initial[0].Position,
                0,
                PlayerColor.Villian,
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

        public int VillianRevealedIn()
            => (Round + Settings.ShowVillianEvery - Settings.ShowVillianAfter)
                    & Settings.ShowVillianEvery;

        public List<Player> players { get; private set; } 
            = new List<Player>();
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
                throw new DomainException("Invalid state");
            }

            CurrentPlayer.MakeTurn(
                targetPosition,
                ticket,
                useDoubleTicket);

            if (CurrentPlayer.Role == PlayerRole.Detective)
            {
                Villian.Tickets.AddTicket(ticket);

                if (CurrentPlayer.Position() == Villian.Position())
                {
                    MatchState = MatchState.DetectivesWon;
                    AddGameEvent(new MatchOverGameEvent(this));
                }
            }
            else
            {
                // it should not be possible to move onto a detective
                Debug.Assert(!Detectives.Any(p => p.Position() == CurrentPlayer.Position()));
                
                ++Round;
                AddGameEvent(new MatchRoundGameEvent());

                if (IsVillianRevealRound)
                {
                    lastStationVillianReveal = Villian.Position().Position;
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
            if (CurrentPlayer.ValidRoutes().Count == 0)
            {
                MatchState = CurrentPlayer.Role == PlayerRole.Detective
                    ? MatchState.VillianWon
                    : MatchState.DetectivesWon;
                AddGameEvent(new MatchOverGameEvent(this));
            }

            AddGameEvent(new MatchTurnGameEvent(this));
        }

        public List<Player> Detectives
            => players.Where(p => p.Role == PlayerRole.Detective).ToList();
        public Player Villian
            => Players.First(p => p.Role == PlayerRole.Villian);
        public Player FirstPlayer
            => Players.Aggregate((p1, p2) => p1.Order < p2.Order ? p1 : p2);
    }
}
