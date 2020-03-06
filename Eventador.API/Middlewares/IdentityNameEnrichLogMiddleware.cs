using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace Eventador.API.Middlewares
{
    [PublicAPI]
    public class IdentityNameEnrichLogMiddleware
    {
        private readonly RequestDelegate _next;

        public IdentityNameEnrichLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            LogContext.PushProperty("IdentityName", context?.User?.Identity?.Name ?? string.Empty);
            await _next(context);
        }
    }

    [PublicAPI]
    public static class IdentityNameEnrichLogMiddlewareExtensions
    {
        public static void UseIdentityNameEnrichLog(this IApplicationBuilder app)
        {
            app.UseMiddleware<IdentityNameEnrichLogMiddleware>();
        }
    }
}
