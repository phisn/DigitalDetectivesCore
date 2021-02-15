using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Attributes;
using Server.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [GameState(State.Collect)]
    public class GameCollectorController : ControllerBase
    {
    }
}
