using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eventador.Services.Contract;

namespace Eventador.API.Authorization
{
    /// <summary>
    /// Авторизационный обработчик для проверки заблокированного пользователя
    /// </summary>
    public class BlockedHandler : AuthorizationHandler<BlockedRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        /// <summary>
        /// ctor
        /// </summary>
        public BlockedHandler(
            IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        /// <inheritdoc />
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BlockedRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var loginClaim = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType);
            if (loginClaim == null)
            {
                return;
            }

            var user = await _userService.GetByLogin(loginClaim.Value);
            if (user == null || user.Blocked)
            {
                return;
            }

            context.Succeed(requirement);
        }
    }
}
