using DigitalDetectivesCore.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Services.Models
{
    public class UserPlayerBinding
    {
        public Guid UserId { get; set; }
        public PlayerColor Color { get; set; }
        public long PlayerId { get; set; }
    }
}
