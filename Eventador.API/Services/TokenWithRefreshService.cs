using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Eventador.Domain.Models;
using Eventador.Services.Options;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Eventador.API.Services
{
    /// <summary>
    /// Сервис для работы с авторизационным JWT с обновлением авторизационного токена
    /// </summary>
    public class TokenWithRefreshService : ITokenWithRefreshService
    {
        /// <summary>
        /// Алгоритм шифрования
        /// </summary>
        private const string SecurityAlgorithm = SecurityAlgorithms.HmacSha256;

        private readonly IRefreshTokenService _refreshTokenService;
        private readonly TokenOptions _tokenOptions;

        /// <summary>
        /// ctor
        /// </summary>
        public TokenWithRefreshService(IRefreshTokenService refreshTokenService, IOptionsMonitor<TokenOptions> tokenOptionsMonitor)
        {
            _refreshTokenService = refreshTokenService;
            _tokenOptions = tokenOptionsMonitor.CurrentValue;
        }

        /// <inheritdoc />
        public TokenModel GetToken(ClaimsIdentity claimsIdentity)
        {
            var token = CreateToken(claimsIdentity);

            AddRefreshToken(GetLogin(claimsIdentity), token.RefreshToken);

            return token;
        }

        /// <inheritdoc />
        public TokenModel GetTokenByRefreshToken(ClaimsIdentity claimsIdentity, string refreshToken)
        {
            var token = CreateToken(claimsIdentity);

            UpdateRefreshToken(GetLogin(claimsIdentity), token.RefreshToken, refreshToken);

            return token;
        }

        /// <inheritdoc />
        public bool ValidRefreshToken(string login, string refreshTokenForCheck)
        {
            return _refreshTokenService.ValidRefreshToken(login, refreshTokenForCheck);
        }

        /// <inheritdoc />
        public void DeleteAllRefreshToken(string login)
        {
            _refreshTokenService.DeleteAllRefreshToken(login);
        }

        /// <summary>
        /// Получить логин по идентификационным данным
        /// </summary>
        /// <param name="claimsIdentity">Идентификационные данные</param>
        /// <returns></returns>
        private static string GetLogin(ClaimsIdentity claimsIdentity)
        {
            var claimLogin = GetClaimLogin(claimsIdentity);
            var login = claimLogin.Value;
            return login;
        }

        /// <summary>
        /// Получить идентификационные данные для логина
        /// </summary>
        /// <param name="claimsIdentity">Идентификационные данные</param>
        /// <returns></returns>
        private static Claim GetClaimLogin(ClaimsIdentity claimsIdentity)
        {
            return claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType);
        }

        /// <summary>
        /// Добавить refresh token пользователю
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="refreshToken">refresh token</param>
        private void AddRefreshToken([NotNull] string login, [NotNull] string refreshToken)
        {
            _refreshTokenService.SetRefreshToken(refreshToken, login, GetExpirationRefreshToken());
        }

        /// <summary>
        /// Обновить refresh token пользователю
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="newRefreshToken">Новый refresh token</param>
        /// <param name="oldRefreshToken">Старый refresh token, который нужно заменить</param>
        private void UpdateRefreshToken([NotNull] string login, [NotNull] string newRefreshToken, [NotNull] string oldRefreshToken)
        {
            _refreshTokenService.SetRefreshToken(newRefreshToken, login, GetExpirationRefreshToken());
            _refreshTokenService.DeleteRefreshToken(login, oldRefreshToken);
        }

        /// <summary>
        /// Создать токен по идентификационным данным
        /// </summary>
        /// <param name="claimsIdentity">Идентификационные данные пользователя</param>
        /// <returns>Токен</returns>
        private TokenModel CreateToken(ClaimsIdentity claimsIdentity)
        {
            var now = DateTimeOffset.UtcNow;
            var expiresAccessToken = now.Add(GetExpirationAccessToken());
            var expiresRefreshToken = now.Add(GetExpirationRefreshToken());

            var accessToken = GetAccessToken(claimsIdentity, now, expiresAccessToken);

            var refreshToken = GetRefreshToken(claimsIdentity, now, expiresRefreshToken);

            var expiresAccessTokenUnixTime = expiresAccessToken.ToUnixTimeSeconds();

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expires = expiresAccessTokenUnixTime
            };
        }

        /// <summary>
        /// Получить авторизационный токен
        /// </summary>
        /// <param name="claimsIdentity">Идентификационные данные</param>
        /// <param name="dateFrom">Дата начала действия токена</param>
        /// <param name="dateExpires">Дата истечения токена</param>
        /// <returns></returns>
        private string GetAccessToken(ClaimsIdentity claimsIdentity, DateTimeOffset dateFrom, DateTimeOffset dateExpires)
        {
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                _tokenOptions.Issuer,
                _tokenOptions.Audience,
                notBefore: dateFrom.UtcDateTime,
                claims: claimsIdentity.Claims,
                expires: dateExpires.UtcDateTime,
                signingCredentials: new SigningCredentials(_tokenOptions.GetSymmetricSecurityKeyForAccess(), SecurityAlgorithm));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            return accessToken;
        }

        /// <summary>
        /// Получить токен для обновления токена авторизации
        /// </summary>
        /// <param name="claimsIdentity">Идентификационные данные</param>
        /// <param name="dateFrom">Дата начала действия токена</param>
        /// <param name="dateExpires">Дата истечения токена</param>
        /// <returns></returns>
        private string GetRefreshToken(ClaimsIdentity claimsIdentity, DateTimeOffset dateFrom, DateTimeOffset dateExpires)
        {
            // создаем JWT Refresh-токен
            var jwtRefresh = new JwtSecurityToken(
                _tokenOptions.Issuer,
                _tokenOptions.Audience,
                notBefore: dateFrom.UtcDateTime,
                claims: claimsIdentity.Claims,
                expires: dateExpires.UtcDateTime,
                signingCredentials: new SigningCredentials(_tokenOptions.GetSymmetricSecurityKeyForRefresh(), SecurityAlgorithm));

            var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtRefresh);
            return refreshToken;
        }

        /// <summary>
        /// Получить время истечения авторизационного токена
        /// </summary>
        /// <returns></returns>
        private TimeSpan GetExpirationAccessToken()
        {
            return TimeSpan.FromSeconds(_tokenOptions.AccessLifetimeSec);
        }

        /// <summary>
        /// Получить время истечения refresh token
        /// </summary>
        /// <returns></returns>
        private TimeSpan GetExpirationRefreshToken()
        {
            return TimeSpan.FromSeconds(_tokenOptions.RefreshLifetimeSec);
        }
    }
}
