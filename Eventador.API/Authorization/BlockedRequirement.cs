using Microsoft.AspNetCore.Authorization;

namespace Eventador.API.Authorization
{
    /// <summary>
    /// Требование для проверки заблокированного пользователя
    /// </summary>
    public class BlockedRequirement : IAuthorizationRequirement
    {
    }
}
