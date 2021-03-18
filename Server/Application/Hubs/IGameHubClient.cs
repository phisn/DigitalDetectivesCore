using Server.Application.Hubs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Hubs
{
    public interface IGameHubClient
    {
        Task OnTurnDetective(TurnDetectiveEvent turnEvent);
        Task OnTurnVillian(TurnVillianEvent turnEvent);
    }
}
