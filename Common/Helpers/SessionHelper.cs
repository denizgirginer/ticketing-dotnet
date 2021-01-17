
using JwtGenerator.ServiceCollection.Extensions.JwtBearer;
using JwtGenerator.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace Ticket.Common.Helpers
{
    public static class SessionHelper {

        public static string GetUserEmail()
        {
            var claimEmail = HttpContext.Current.User.FindFirst(x => x.Type == ClaimTypes.Email);
            return claimEmail?.Value;
        }


        public static string GetUserId()
        {
            var claimEmail = HttpContext.Current.User.FindFirst(x => x.Type == "userId");
            return claimEmail?.Value;
        }

        public static IServiceCollection AddJwtForAPI(
            this IServiceCollection services)
        {
            var tokenOptions = new TokenOptions(
                Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Environment.GetEnvironmentVariable("JWT_KEY"), 30);

            services.AddJwtAuthenticationForAPI(tokenOptions);

            return services;
        }

        public static IApplicationBuilder UseHttpContext(
            this IApplicationBuilder services, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            HttpContext.Configure(httpContextAccessor);

            return services;
        }

    }


    public static class HttpContext
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor m_httpContextAccessor;


        public static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            m_httpContextAccessor = httpContextAccessor;
        }

        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                return m_httpContextAccessor.HttpContext;
            }
        }

    }
}