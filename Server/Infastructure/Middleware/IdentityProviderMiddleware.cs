using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Infastructure.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class IdentityProviderMiddleware
    {
        private readonly RequestDelegate _next;

        public IdentityProviderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string identity = httpContext.Request.Cookies["identity"];

            if (identity == null)
            {
                identity = Guid.NewGuid().ToString();
                httpContext.Response.Cookies.Append("identity", identity);
            }

            httpContext.Items["identity"] = identity;

            return _next(httpContext);
        }
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
