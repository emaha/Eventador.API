using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace Eventador.API.Middlewares
{
    /// <summary>
    ///
    /// </summary>
    [PublicAPI]
    public class IdentityNameEnrichLogMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        ///
        /// </summary>
        /// <param name="next"></param>
        public IdentityNameEnrichLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            LogContext.PushProperty("IdentityName", context?.User?.Identity?.Name ?? string.Empty);
            await _next(context);
        }
    }

    /// <summary>
    ///
    /// </summary>
    [PublicAPI]
    public static class IdentityNameEnrichLogMiddlewareExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        public static void UseIdentityNameEnrichLog(this IApplicationBuilder app)
        {
            app.UseMiddleware<IdentityNameEnrichLogMiddleware>();
        }
    }
}
