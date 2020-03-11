using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Eventador.API.Middlewares
{
    /// <summary>
    /// Middleware для аутентификации через токен
    /// </summary>
    [Obsolete("Используется другой способ авторизации")]
    public class TokenAuthenticationMiddleware
    {
        private const string IdentityName = "Service";
        private const string AuthorizationHeader = "Authorization";
        private const string AuthenticateScheme = "Bearer";
        private const string Realm = "SalePoint Api";

        private const string AuthenticateToken =
            "UwPmRxALPvH5EfaxtNd5KPYTMz2OhZ5lcAkVvZ3duy7upjctFOexD2t33a5CehV7a8ZKOZINKQX3b6l7QtzgYzx0RFyzgrgrv39";

        private readonly RequestDelegate _next;

        private static readonly string[] excludePaths = { "/health", "/version" };

        /// <summary>
        /// ctor
        /// </summary>
        public TokenAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// InvokeAsync
        /// </summary>
        [UsedImplicitly]
        public async Task InvokeAsync(HttpContext context)
        {
            // Исключаем для путей требование авторизации
            if (excludePaths.Any(path => context.Request.Path.StartsWithSegments(path)))
            {
                await _next.Invoke(context);
                return;
            }

            StringValues authHeader = context.Request.Headers[AuthorizationHeader];

            if (authHeader.Count > 0)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                if (authHeaderVal.Scheme.Equals(AuthenticateScheme, StringComparison.OrdinalIgnoreCase)
                    && authHeaderVal.Parameter != null)
                {
                    AuthenticateUser(context, authHeaderVal.Parameter);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.Headers.Add("WWW-Authenticate", string.Format($"{AuthenticateScheme} realm=\"{Realm}\""));
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        private static void SetPrincipal(HttpContext context, GenericPrincipal principal)
        {
            if (context.User != null)
            {
                context.User = principal;
            }
        }

        private static bool CheckToken(string token)
        {
            if (string.IsNullOrWhiteSpace(AuthenticateToken))
            {
                return false;
            }

            return AuthenticateToken.Equals(token, StringComparison.InvariantCulture);
        }

        private static void AuthenticateUser(HttpContext context, string token)
        {
            try
            {
                if (CheckToken(token))
                {
                    var identity = new GenericIdentity(IdentityName, AuthenticateScheme);
                    SetPrincipal(context, new GenericPrincipal(identity, null));
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }
}
