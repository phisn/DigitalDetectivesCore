using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Application.Services
{
    public interface IIdentityService
    {
        Guid User { get; }
    }
}
