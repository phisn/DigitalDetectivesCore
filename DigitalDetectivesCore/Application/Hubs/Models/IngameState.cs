using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Hubs.Models
{
    public class IngameState
    {
        public IngameStateType Type { get; set; }
        // can be any type of playerstate
        public PlayerState State { get; set; }
    }
}
