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

        public Task<MatchInfo> GetMatchInfo()
        {
            return Task.FromResult(new MatchInfo
            {
                AvailableColors = ingameService.ColorsUnregistered,
                Round = ingameService.Match.Round,
                Settings = ingameService.Match.Settings
            });
        }

        public async Task RegisterAsPlayer(PlayerColor? color)
        {
            if (ingameUserService.Registered)
            {
                if (color == null || ingameUserService.Player.Color == color.Value)
                {
                    return;
                }

                ingameService.UnregisterUser(identityService.User);
            }

            try
            {
                if (color.HasValue)
                {
                    ingameService.RegisterUser(
                        identityService.User,
                        color.Value);
                }
                else
                {
                    ingameService.RegisterUser(
                        identityService.User);
                }

                await Clients.Caller.UpdateStateByPlayer(
                    ingameUserService.Player);
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
                ingameUserService.MakeTurn(position, ticket, useDoubleTicket);
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
            logger.LogDebug($"User disconnected ({identityService.User.ToString()} | {(ingameUserService.Registered ? ingameUserService.Player.Color.ToString() : "unregistered")})");
            ingameService.UnregisterUser(identityService.User);
            return base.OnDisconnectedAsync(exception);
        }

        private ILogger<IngameHub> logger;
        private IIdentityService identityService;
        private IIngameService ingameService;
        private IIngameUserService ingameUserService;
    }
}
