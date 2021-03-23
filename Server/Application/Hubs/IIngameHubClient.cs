using Server.Application.Hubs.Models;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Hubs
{
    public interface IIngameHubClient
    {
        Task UpdateStateDetective(DetectiveState state);
        Task UpdateStateVillian(VillianState state);

        Task MatchStartedEvent(MatchInfo info);
        Task MatchCanceldEvent();
        Task NewRoundEvent();
        Task PlayerLeftEvent(PlayerColor color);
        Task PlayerJoinedEvent(PlayerColor color);
    }
}
