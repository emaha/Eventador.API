using Eventador.API.Authorization;
using Eventador.API.Middlewares;
using Eventador.API.Services;
using Eventador.Common.JsonSerializer;
using Eventador.Common.Middlewares;
using Eventador.Data;
using Eventador.Data.Contract;
using Eventador.Data.Repositories;
using Eventador.Services;
using Eventador.Services.Contract;
using Eventador.Services.Contract.Api;
using Eventador.Services.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using Refit;
using Serilog;
using System;
using System.Globalization;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Eventador.API
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
            //Serilog for DI
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            // Конфигурация БД контекста
            string connectionString = Configuration["DbConfig:DbConnectionStrings:Eventador"];
            services.AddDbContext<EventadorDbContext>(options => options.UseNpgsql(connectionString
                    , x => x.MigrationsAssembly("Eventador.Data.Migrations")));

            // Добавление компрессии на сайт с использованием Gzip
            //
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.AddResponseCompression(options => { options.EnableForHttps = true; });

            var supportedCultures = new[] { new CultureInfo("ru-RU") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("ru-RU");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            // Для загрузки больших файлов на сервер
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = 10 * 1024 * 1024;
                x.MultipartBodyLengthLimit = 10 * 1024 * 1024;
            });

            RegisterApi(services);
            RegisterServices(services);
            RegisterRepositories(services);

            RegisterAuthentication(services);
            RegisterAuthorization(services);
            RegisterOptions(services);

            RegisterRemoteServices(services);
            RegisterHealthChecks(services);

            RegisterSwagger(services);
            RegisterDistributedCache(services);

            services.AddControllers().AddJsonOptions(options => { JsonSerializer.CreateOptionsByDefault(options); });
        }

        private void RegisterDistributedCache(IServiceCollection services)
        {
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
        }

        private void RegisterSwagger(IServiceCollection services)
        {
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

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}eventador.api.xml");
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eventador API V1"); });
            }

            //app.UseForwardedHeaders(new ForwardedHeadersOptions
            //{
            //    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            //});

            app.UseResponseCompression();
            app.UseRequestLocalization();

            app.UseIpEnrichLog();
            app.UseIdentityNameEnrichLog();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseMiddleware<TokenAuthenticationMiddleware>();

            app.UseVersion("/version");

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
            services.AddOptions();
            services.Configure<ServiceOption>(Configuration.GetSection("ServiceOption"));
            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMarkRepository, MarkRepository>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMarkService, MarkService>();
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
        /// Регистрация аутентификации
        /// </summary>
        /// <param name="services"></param>
        private void RegisterAuthentication(IServiceCollection services)
        {
            RegisterTokenService(services);

            var tokenOptions = new TokenOptions();
            Configuration.GetSection("TokenOptions").Bind(tokenOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(op =>
                {
                    if (!Environment.IsProduction())
                    {
                        op.RequireHttpsMetadata = false;
                    }

                    op.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Валидация издателя
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = tokenOptions.Issuer,

                        // Валидация потребителя токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = tokenOptions.Audience,

                        // Валидация время жизни токена
                        ValidateLifetime = true,

                        // Установка ключа безопасности
                        IssuerSigningKey = tokenOptions.GetSymmetricSecurityKeyForAccess(),
                        // Валидация ключа безопасности
                        ValidateIssuerSigningKey = true
                    };
                });
        }

        /// <summary>
        /// Регистрация сервисов для работы с токеном
        /// </summary>
        private static void RegisterTokenService(IServiceCollection services)
        {
            services.AddScoped<ITokenWithRefreshService, TokenWithRefreshService>();
            services.AddScoped<IRefreshTokenService, CacheRefreshTokenService>();
        }

        /// <summary>
        /// Регистрация авторизации
        /// </summary>
        private static void RegisterAuthorization(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            RegisterAuthorizationHandler(services);

            services.AddAuthorization(options =>
            {
                // Политика для проверки, что пользователь заблокирован, пока у него активная аутентификация (активный токен)
                options.AddPolicy(AuthorizationPolicyName.Blocked, policy =>
                    policy.Requirements.Add(new BlockedRequirement()));
            });
        }

        /// <summary>
        /// Регистрация обработчиков авторизации
        /// </summary>
        private static void RegisterAuthorizationHandler(IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, BlockedHandler>();
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
