using Server.Application.Hubs.Models;
using Server.Game.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Hubs
{
    public static class IIngameHubClientExtensions
    {
        public static async Task UpdateStateByPlayer(this IIngameHubClient client, Player player)
        {
            if (player.Role == PlayerRole.Detective)
            {
                await client.UpdateStateDetective(DetectiveInfoFromPlayer(player));
            }
            else
            {
                await client.UpdateStateVillian(VillianInfoFromPlayer(player));
            }
        }

        private static DetectiveState DetectiveInfoFromPlayer(Player player)
            => new DetectiveState(
                player.IsCurrentPlayer()
                    ? RoutesForPlayer(player)
                    : new List<RouteOption>(),
                player.Position().Position,
                player.Match.VillianRevealedIn(),
                player.Tickets,
                player.Match.Villian.Tickets);

        private static VillianState VillianInfoFromPlayer(Player player)
            => new VillianState(
                player.Id == player.Match.CurrentPlayerId
                    ? RoutesForPlayer(player)
                    : new List<RouteOption>(),
                player.Position().Position,
                player.Match.VillianRevealedIn(),
                player.Tickets,
                player.Match.Detectives
                    .Select(p => new TicketsForPlayer
                    {
                        Color = p.Color,
                        Tickets = p.Tickets
                    })
                    .ToList());

        private static List<RouteOption> RoutesForPlayer(Player player)
            => player.ValidRoutes()
                .Select(r => new RouteOption
                {
                    Position = r.To.Position,
                    Type = r.Type
                })
                .ToList();
    }
}
