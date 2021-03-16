using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class IdentityService : IIdentityService
    {
        public Guid User { get; private set; }

        public IdentityService(IHttpContextAccessor accessor)
        {
            string identity = (string) accessor.HttpContext.Items["identity"];

            // identity should already be ensured to be not null by
            // identityprovidermiddleware
            if (identity == null)
                throw new Exception("Missing identity");

            User = Guid.Parse(identity);
        }
    }
}
