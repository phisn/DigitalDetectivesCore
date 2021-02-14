using Microsoft.Extensions.Hosting;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.ServiceWorker
{
    public class TemperatureWorker : BackgroundService
    {
        public TemperatureWorker(IBoard board)
        {
            this.board = board;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            board.FanEnabled = false;

            if (board.FanEnabled)
            {
                if (board.CpuTemperature < MIN_TEMP)
                {
                    board.FanEnabled = false;
                }
            }
            else
            {
                if (board.CpuTemperature > MAX_TEMP)
                {
                    board.FanEnabled = true;
                }
            }

            throw new NotImplementedException();
        }

        private const float MAX_TEMP = 55.0f;
        private const float MIN_TEMP = 40.0f;

        private IBoard board;
    }
}
