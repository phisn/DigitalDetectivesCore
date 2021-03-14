using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Controller
{
    [ApiController]
    public class GameController : ControllerBase
    {
        public IActionResult Start()
        {


            return Ok();
        }
    }
}
