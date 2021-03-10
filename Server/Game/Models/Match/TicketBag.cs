using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game
{
    public class TicketBag
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

        public int
            Yellow,
            Green,
            Red,
            Black,
            Double;
    }

    public enum TicketType
    {
        Yellow,
        Green,
        Red,
        Black,
        Double
    }
}
