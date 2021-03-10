using Iot.Device.Ws28xx;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Device.Spi;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;

namespace Server.Services
{
    public class RaspberryHardwareService : IHardwareService
    {
        private const int PIN_FAN = 4;

        public bool FanEnabled 
        {
            get => Pi.Gpio[PIN_FAN].Value;
            set => Pi.Gpio[PIN_FAN].Value = value;
        }

        public RaspberryHardwareService()
        {
            Pi.Gpio[PIN_FAN].PinMode = GpioPinDriveMode.Output;
        }

        public float CpuTemperature => 
            int.Parse(File.ReadAllText("/sys/class/thermal/thermal_zone0/temp")) / 1000.0f;
    }
}
