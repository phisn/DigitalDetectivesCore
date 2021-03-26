using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Interfaces
{
    public interface IHardwareService
    {
        public bool FanEnabled { get; set; }
        public float CpuTemperature { get; }
    }
}
