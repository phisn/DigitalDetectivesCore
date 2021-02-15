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
    public class PhaseFilterAttribute : ActionFilterAttribute
    {

        public PhaseFilterAttribute(Phase phase)
        {
            this.Phase = phase;
        }

        public Phase Phase { get; private set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IGameStateService gameState = context.HttpContext.RequestServices.GetService<IGameStateService>();

            if (gameState.CurrentPhase != Phase)
            {
                context.Result = new BadRequestResult();
                return;
            }

            await next();
        }
    }
}
