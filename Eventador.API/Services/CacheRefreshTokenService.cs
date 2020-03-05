using Eventador.Common.Extensions;
using Eventador.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Eventador.API.Services
{
    /// <summary>
    /// Сервис для работы с RefreshToken в кэш
    /// </summary>
    public class CacheRefreshTokenService : IRefreshTokenService
    {
        private readonly IDistributedCache _cache;

        /// <summary>
        /// Максимальное количество хранимых refresh токенов для одного пользователя
        /// </summary>
        private const int MaxCountStoredTokensPerUser = 10;

        private const string CacheKey = "RefreshToken";

        /// <summary>
        /// ctor
        /// </summary>
        public CacheRefreshTokenService(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <inheritdoc />
        public void SetRefreshToken(string refreshToken, string login, TimeSpan cacheExpiration)
        {
            var cacheKey = GetCacheKey(login);

            var refreshTokens = GetJwtRefreshTokens(cacheKey).ToList();
            if (refreshTokens.Count() < MaxCountStoredTokensPerUser)
            {
                refreshTokens.Add(refreshToken);
            }
            else
            {
                refreshTokens = refreshTokens.Skip(1).ToList();
                refreshTokens.Add(refreshToken);
            }

            _cache.SetObject(cacheKey, refreshTokens, cacheExpiration);
        }

        /// <inheritdoc />
        public bool ValidRefreshToken(string login, string refreshToken)
        {
            var cacheKey = GetCacheKey(login);
            var cacheRefreshTokens = _cache.GetObject<string[]>(cacheKey) ?? new string[0];
            return cacheRefreshTokens.Any() && cacheRefreshTokens.Contains(refreshToken);
        }

        /// <inheritdoc />
        public void DeleteAllRefreshToken(string login)
        {
            var cacheKey = GetCacheKey(login);
            _cache.Remove(cacheKey);
        }

        /// <inheritdoc />
        public void DeleteRefreshToken(string login, string refreshToken)
        {
            var cacheKey = GetCacheKey(login);

            var storedRefreshTokens = GetStoredRefreshTokens(cacheKey).ToArray();

            // получаем дату истечения для ключа кэша по последнему актуальному токену
            var cacheExpiration = new JwtSecurityToken(storedRefreshTokens.Last()).ValidTo - DateTime.UtcNow;

            // Исключить токен
            var tokens = storedRefreshTokens
                .Where(x => x != refreshToken)
                .ToArray();

            _cache.SetObject(cacheKey, tokens, cacheExpiration);
        }

        /// <summary>
        /// Получить JWT токен refresh`а
        /// </summary>
        /// <param name="cacheKey">Ключ для получения из кэша</param>
        /// <returns></returns>
        private IEnumerable<string> GetJwtRefreshTokens([NotNull] string cacheKey)
        {
            var cacheRefreshToken = GetStoredRefreshTokens(cacheKey);

            // Удаляем просроченные токены
            //
            var refreshTokens = cacheRefreshToken
                .Select(x => new JwtSecurityToken(x))
                .Where(x => x.ValidTo > DateTime.UtcNow)
                .Select(x => x.RawData)
                ;
            return refreshTokens;
        }

        /// <summary>
        /// Получить сохраненные refresh token`ы
        /// </summary>
        /// <param name="cacheKey">Ключ для получения из кэша</param>
        /// <returns></returns>
        private IEnumerable<string> GetStoredRefreshTokens(string cacheKey)
        {
            return _cache.GetObject<string[]>(cacheKey) ?? new string[0];
        }

        /// <summary>
        /// Получить ключ для кэша
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private static string GetCacheKey([NotNull] string login)
        {
            return CacheKey + login;
        }
    }
}
