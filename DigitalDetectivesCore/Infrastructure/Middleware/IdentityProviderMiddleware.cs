using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDetectivesCore.Infrastructure.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class IdentityProviderMiddleware
    {
        private readonly RequestDelegate _next;

        public IdentityProviderMiddleware(
            RequestDelegate next,
            ILogger<IdentityProviderMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string identity = httpContext.Request.Cookies["identity"];

            if (identity == null)
            {
                identity = Guid.NewGuid().ToString();
                httpContext.Response.Cookies.Append("identity", identity);
                logger.LogInformation($"created identity {identity}");
            }

            httpContext.Items["identity"] = identity;

            return _next(httpContext);
        }

        private ILogger<IdentityProviderMiddleware> logger;
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class IdentityProviderMiddlewareExtensions
    {
        public static IApplicationBuilder UseIdentityProviderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdentityProviderMiddleware>();
        }
    }
}
