using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.ServiceWorker
{
    public class GameWorker : BackgroundService
    {
        public GameWorker(ILogger<GameWorker> logger, IBoard boardService)
        {
            this.logger = logger;
            this.boardService = boardService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            boardService.FanEnabled = true;

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("New Worker running at: {time}", DateTimeOffset.Now);
                //logger.LogInformation($"{boardService.CpuTemperature}");
                await Task.Delay(1000, stoppingToken);
            }
        }

        private IBoard boardService;
        private ILogger<GameWorker> logger;
    }
}
