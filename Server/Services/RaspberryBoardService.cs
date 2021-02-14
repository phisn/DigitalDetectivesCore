using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;

namespace Server.Services
{
    public class RaspberryBoardService : IBoard
    {
        public bool FanEnabled 
        {
            get => Pi.Gpio[PIN_FAN].Value;
            set => Pi.Gpio[PIN_FAN].Value = value;
        }

        public RaspberryBoardService()
        {
            Pi.Init<BootstrapWiringPi>();
            Pi.Gpio[PIN_FAN].PinMode = GpioPinDriveMode.Output;
        }

        private int PIN_FAN => 4;

        public float CpuTemperature => throw new NotImplementedException();
    }
}
