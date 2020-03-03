using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace Eventador.Common.Middlewares
{
    [PublicAPI]
    public class IpEnrichLogMiddleware
    {
        private readonly RequestDelegate _next;

        public IpEnrichLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            LogContext.PushProperty("IPAddress", context?.Connection?.RemoteIpAddress);
            await _next(context);
        }
    }

    [PublicAPI]
    public static class IpEnrichLogMiddlewareExtensions
    {
        public static void UseIpEnrichLog(this IApplicationBuilder app)
        {
            app.UseMiddleware<IpEnrichLogMiddleware>();
        }
    }
}
