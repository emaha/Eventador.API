using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Eventador.API.Middlewares
{
    /// <summary>
    /// Мидлвар добавляющий endpoint /version c возвратом версии приложения.
    /// Версия и имя достаются из конфигурации по переменным "App" и "Version".
    /// <remarks>
    /// </remarks>
    /// </summary>
    public static class VersionMiddleware
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        public static void HandleMapVersion(IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var env = app.ApplicationServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;

            app.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                context.Response.Headers.Append("Cache-Control", "no-store, no-cache");

                var version = new
                {
                    App = configuration?["App"] ?? env?.ApplicationName,
                    Version = configuration?["Version"] ?? "none",
                    Env = env?.EnvironmentName ?? "none"
                };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(version));
            });
        }
    }

    /// <summary>
    ///
    /// </summary>
    public static class VersionMiddlewareExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="path"></param>
        public static void UseVersion(this IApplicationBuilder app, PathString path)

        {
            app.Map(path, VersionMiddleware.HandleMapVersion);
        }
    }
}
