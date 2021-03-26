using DigitalDetectivesCore.Game.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Game.Models.Match
{
    public class MatchSettings : ValueObject
    {
        public static MatchSettings Default =>
            new MatchSettings(
                4, 24, 2, 5,
                TicketBag.Detective, TicketBag.Villian,
                1.0f);

        public MatchSettings(
            int playerCount, 
            int rounds, 
            int showVillianAfter, 
            int showVillianEvery, 
            TicketBag detectiveTickets, 
            TicketBag villianTickets, 
            float villianBlackTicketMulti)
        {
            PlayerCount = playerCount;
            Rounds = rounds;
            ShowVillianAfter = showVillianAfter;
            ShowVillianEvery = showVillianEvery;
            this.detectiveTickets = detectiveTickets;
            this.villianTickets = villianTickets;
            VillianBlackTicketMulti = villianBlackTicketMulti;
        }

        public int PlayerCount { get; }
        public int Rounds { get; }
        public int ShowVillianAfter { get; }
        public int ShowVillianEvery { get; }
        
        private TicketBag detectiveTickets;
        public TicketBag DetectiveTickets => new TicketBag(detectiveTickets);

        private TicketBag villianTickets;
        public TicketBag VillianTickets => new TicketBag(villianTickets,
            (int) Math.Round((PlayerCount - 1) * VillianBlackTicketMulti));

        public float VillianBlackTicketMulti { get; set; }

        public bool Valid() =>
            (PlayerCount >= 3 || PlayerCount <= 6) &&
            (Rounds > 0) &&
            (ShowVillianAfter >= 0) &&
            (ShowVillianEvery >= 0) &&
            // villianblackticketmulti can be negative
            (VillianTickets.Black + VillianBlackTicketMulti * (PlayerCount - 1) >= 0) &&
            (VillianTickets.Valid()) &&
            (DetectiveTickets.Valid());

        public override bool Equals(object obj)
        {
            return obj is MatchSettings settings &&
                Rounds == settings.Rounds &&
                ShowVillianAfter == settings.ShowVillianAfter &&
                ShowVillianEvery == settings.ShowVillianEvery &&
                detectiveTickets.Equals(settings.detectiveTickets) &&
                villianTickets.Equals(settings.villianTickets) &&
                VillianBlackTicketMulti == settings.VillianBlackTicketMulti;
        }
    }
}
