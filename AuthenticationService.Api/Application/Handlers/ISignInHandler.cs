using AuthenticationService.Api.Application.Models;
using AuthenticationService.Infrastructure.Entities;
using System.Threading.Tasks;

namespace AuthenticationService.Api.Application.Handlers
{
    /// <summary>
    /// Сервис авторизации.
    /// </summary>
    public interface ISignInHandler
    {
        /// <summary>
        /// Генерирует авторизационный токен.
        /// </summary>
        Task<TokenModel> GetToken(SignInEntity request);
    }
}
