using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Server.Game;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Attributes
{
    public class GameStateAttribute : ActionFilterAttribute
    {
        public GameStateAttribute(State state)
        {
            this.state = state;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IGameStorageService gameStorage = context.HttpContext.RequestServices.GetService<IGameStorageService>();

            if (gameStorage.CurrentState != state)
            {
                context.Result = new BadRequestResult();
                return;
            }

            await next();
        }

        private State state;
    }
}
