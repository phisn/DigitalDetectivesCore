using Server.Game.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Models.Match
{
    public class TicketBag : ValueObject
    {
        public const int 
            DetectiveYellow = 10,
            DetectiveGreen = 8,
            DetectiveRed = 4,
            
            VillianYellow = 4,
            VillianGreen = 3,
            VillianRed = 3,
            VillianDouble = 2;

        public static TicketBag Villian
            => new TicketBag(
                VillianYellow,
                VillianGreen,
                VillianRed,
                0,
                VillianDouble);

        public static TicketBag Detective
            => new TicketBag(
                DetectiveYellow,
                DetectiveGreen,
                DetectiveRed,
                0, 0);

        public TicketBag(
            TicketBag ticketBag,
            int additionalBlackTickets)
            :
            this(ticketBag)
        {
            Black += additionalBlackTickets;
        }

        public TicketBag(TicketBag ticketBag)
            :
            this(
                ticketBag.Yellow,
                ticketBag.Green,
                ticketBag.Red,
                ticketBag.Black,
                ticketBag.Double)
        {
        }

        public TicketBag(
            int yellowTicket,
            int greenTicket,
            int redTicket,
            int blackTicket,
            int doubleTicket)
        {
            Yellow = yellowTicket;
            Green = greenTicket;
            Red = redTicket;
            Black = blackTicket;
            Double = doubleTicket;
        }

        public int Yellow { get; private set; }
        public int Green { get; private set; }
        public int Red { get; private set; }
        public int Black { get; private set; }
        public int Double { get; private set; }

        public bool Valid() =>
            Yellow >= 0 &&
            Green >= 0 &&
            Red >= 0 &&
            Black >= 0 &&
            Double >= 0;

        public void AddTicket(TicketType ticket)
        {
            switch (ticket)
            {
                case TicketType.Yellow:
                    ++Yellow;
                    break;
                case TicketType.Green:
                    ++Green;
                    break;
                case TicketType.Red:
                    ++Red;
                    break;
                case TicketType.Black:
                    ++Black;
                    break;
            }
        }

        public void RemoveTicket(TicketType ticket)
        {
            switch (ticket)
            {
                case TicketType.Yellow:
                    --Yellow;
                    break;
                case TicketType.Green:
                    --Green;
                    break;
                case TicketType.Red:
                    --Red;
                    break;
                case TicketType.Black:
                    --Black;
                    break;
            }
        }

        public void RemoveDoubleTicket()
            => --Double;

        public override bool Equals(object obj)
        {
            return obj is TicketBag bag &&
                   Yellow == bag.Yellow &&
                   Green == bag.Green &&
                   Red == bag.Red &&
                   Black == bag.Black &&
                   Double == bag.Double;
        }
    }

    public enum TicketType
    {
        Yellow,
        Green,
        Red,
        Black
    }
}
