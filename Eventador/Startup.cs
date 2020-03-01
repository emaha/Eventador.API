using Eventador.Common.Middlewares;
using Eventador.Data;
using Eventador.Data.Contract;
using Eventador.Data.Repositories;
using Eventador.Services;
using Eventador.Services.Contract;
using Eventador.Services.Contract.Api;
using Eventador.Services.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using Refit;
using Serilog;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Eventador
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private const string ConfigurationConnectionStringRedis = "Redis:ConnectionString";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("serilog.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"serilog.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = env;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        /// <summary>
        /// Environment
        /// </summary>
        private IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Конфигурация БД контекста
            //
            string connectionString = Configuration["DbConfig:DbConnectionStrings:Eventador"];
            services.AddDbContext<EventadorDbContext>(options => options.UseNpgsql(connectionString
                    , x => x.MigrationsAssembly("Eventador")));

            RegisterOptions(services);

            RegisterHealthChecks(services);

            RegisterServices(services);
            RegisterRepositories(services);

            RegisterRemoteServices(services);

            RegisterApi(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Eventador. API",
                    Description = "Api"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}eventador.api.xml");
            });

            if (Environment.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddDistributedRedisCache(option =>
                {
                    option.Configuration = Configuration[ConfigurationConnectionStringRedis];
                    option.InstanceName = "eventador:";
                });
            }

            services.AddControllers();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eventador API V1"); });
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseIpEnrichLog();

            var supportedCultures = new[] { new CultureInfo("ru-RU") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru-RU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterApi(IServiceCollection services)
        {
            var serviceOption = new ServiceOption();
            Configuration.GetSection("ServiceOptions").Bind(serviceOption);

            services.AddRefitClient<IServiceApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(serviceOption.Server);
                    c.Timeout = TimeSpan.FromSeconds(serviceOption.TimeoutSec);
                    c.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(serviceOption.Scheme, serviceOption.Token);
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());
        }

        private void RegisterOptions(IServiceCollection services)
        {
            services.Configure<ServiceOption>(Configuration.GetSection("ServiceOption"));
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
        }

        /// <summary>
        /// Базовая политика повторов с колебанием задержки
        /// <remarks>Политики, заданные с помощью этого метода расширения, обрабатывают HttpRequestException,
        /// ответы HTTP 5xx и ответы HTTP 408</remarks>
        /// </summary>
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var jitterer = new Random();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(3, sleep => TimeSpan.FromSeconds(1)
                                               + TimeSpan.FromMilliseconds(jitterer.Next(0, 100)));
        }

        private void RegisterRemoteServices(IServiceCollection services)
        {
            //services.AddScoped<IRemoteService, RemoteService>();
        }

        /// <summary>
        /// Регистрация сервиса мониторинга работоспособности
        /// </summary>
        /// <param name="services"></param>
        private void RegisterHealthChecks(IServiceCollection services)
        {
            var healthChecks = services.AddHealthChecks();

            //healthChecks.AddNpgSql(
            //    Configuration[ConfigurationConnectionStringEventador],
            //    name: "myDataBase", failureStatus: HealthStatus.Unhealthy
            //);

            //healthChecks.AddRedis(Configuration[ConfigurationConnectionStringRedis], "redis",
            //    HealthStatus.Unhealthy, new[] { "redis" });
        }
    }
}
