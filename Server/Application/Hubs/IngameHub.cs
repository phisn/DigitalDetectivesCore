using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Server.Application.Hubs.Models;
using Server.Application.Services;
using Server.Game.Models.Match;
using Server.Game.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Hubs
{
    public class IngameHub : Hub<IIngameHubClient>
    {
        public IngameHub(
            ILogger<IngameHub> logger,
            IIdentityService identityService,
            IIngameService ingameService,
            IIngameUserService ingameUserService)
        {
            this.logger = logger;
            this.identityService = identityService;
            this.ingameService = ingameService;
            this.ingameUserService = ingameUserService;
        }

        public async Task<MatchInfo> GetMatchInfo()
        {
            Match match = await ingameService.GetMatch();

            return new MatchInfo
            {
                AvailableColors = await ingameService.GetColorsUnregistered(),
                Round = match.Round,
                Settings = match.Settings
            };
        }

        public async Task RegisterAsPlayer(PlayerColor? color)
        {
            if (ingameUserService.Registered)
            {
                Player player = await ingameUserService.GetPlayer();

                if (color == null || player.Color == color.Value)
                {
                    return;
                }

                await ingameService.UnregisterUser(identityService.User);
            }

            try
            {
                if (color.HasValue)
                {
                    await ingameService.RegisterUser(
                        identityService.User,
                        color.Value);
                }
                else
                {
                    await ingameService.RegisterUser(
                        identityService.User);
                }

                await Clients.Caller.UpdateStateByPlayer(
                    await ingameUserService.GetPlayer());
            }
            catch (Exception e)
            {
                logger.LogError($"RegisterAsPlayer failed with exception ({e.Message}) ({e.Message})");
                throw new HubException("Failed to register as player");
            }
        }

        public async Task MakeTurn(long position, TicketType ticket, bool useDoubleTicket)
        {
            try
            {
                await ingameUserService.MakeTurn(position, ticket, useDoubleTicket);
            }
            catch (DomainException e)
            {
                logger.LogError($"MakeTurn failed with domainexception ({identityService.User}) ({e.Message}) ({e.StackTrace})");
                throw new HubException("Failed to make turn");
            }
            catch (Exception e)
            {
                logger.LogError($"MakeTurn failed with exception ({identityService.User}) ({e.Message}) ({e.StackTrace})");
                throw new HubException("Failed to make turn");
            }
        }

        public async override Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                identityService.User.ToString());
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            logger.LogDebug($"User disconnected ({identityService.User} | {(ingameUserService.Registered ? ingameUserService.Binding.Color : "unregistered")})");
            ingameService.UnregisterUser(identityService.User);
            return base.OnDisconnectedAsync(exception);
        }

        private ILogger<IngameHub> logger;
        private IIdentityService identityService;
        private IIngameService ingameService;
        private IIngameUserService ingameUserService;
    }
}
