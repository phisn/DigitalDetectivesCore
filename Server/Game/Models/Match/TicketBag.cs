using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.Models.Match
{
    public struct TicketBag
    {
        public const int 
            DetectiveYellow = 10,
            DetectiveGreen = 8,
            DetectiveRed = 4,
            
            VillianYellow = 4,
            VillianGreen = 3,
            VillianRed = 3,
            VillianDouble = 2;

        public static TicketBag Villian(int blackTicket)
            => new TicketBag
            {
                Yellow = VillianYellow,
                Green = VillianGreen,
                Red = VillianRed,
                Black = blackTicket,
                Double = VillianDouble
            };

        public static TicketBag Detective()
            => new TicketBag
            {
                Yellow = DetectiveYellow,
                Green = DetectiveGreen,
                Red = DetectiveRed,
                Black = 0,
                Double = 0
            };

        public static TicketBag Custom(
            int yellowTicket,
            int greenTicket,
            int redTicket,
            int blackTicket,
            int doubleTicket)
            => new TicketBag
            {
                Yellow = yellowTicket,
                Green = greenTicket,
                Red = redTicket,
                Black = blackTicket,
                Double = doubleTicket
            };

        public int Yellow { get; private set; }
        public int Green { get; private set; }
        public int Red { get; private set; }
        public int Black { get; private set; }
        public int Double { get; private set; }

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
    }

    public enum TicketType
    {
        Yellow,
        Green,
        Red,
        Black
    }
}
