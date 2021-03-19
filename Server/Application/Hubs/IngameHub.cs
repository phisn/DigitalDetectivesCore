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
            IngameUserService userIngameService)
        {
            this.logger = logger;
            this.identityService = identityService;
            this.userIngameService = userIngameService;
        }

        public async Task MakeTurn(long position, TicketType ticket, bool useDoubleTicket)
        {
            try
            {
                userIngameService.MakeTurn(position, ticket, useDoubleTicket);
            }
            catch (DomainException e)
            {
                logger.LogError($"MakeTurn failed with domainexception ({e.Message}) ({e.StackTrace})");
                await Clients.Caller.ErrorEvent("DomainException");
                await Clients.Caller.UpdateStateByPlayer(userIngameService.Player);
            }
            catch (Exception e)
            {
                logger.LogError($"MakeTurn failed with exception ({e.Message}) ({e.StackTrace})");
                await Clients.Caller.ErrorEvent("Exception");
                await Clients.Caller.UpdateStateByPlayer(userIngameService.Player);
            }
        }

        public async override Task OnConnectedAsync()
        {
            if (!userIngameService.Registered)
            {
                logger.LogWarning($"Unregistered user tried to connect ({identityService.User.ToString()})");

                // use filter in future
                Context.Abort();
            }

            await base.OnConnectedAsync();

            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                identityService.User.ToString());

            await Clients.Caller.UpdateStateByPlayer(userIngameService.Player);

            logger.LogDebug($"User connected ({identityService.User.ToString()} | {userIngameService.Player.Color.ToString()})");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            logger.LogDebug($"User disconnected ({identityService.User.ToString()} | {(userIngameService.Registered ? userIngameService.Player.Color.ToString() : "unregistered")})");
            return base.OnDisconnectedAsync(exception);
        }

        private ILogger<IngameHub> logger;
        private IIdentityService identityService;
        private IngameUserService userIngameService;
    }
}
