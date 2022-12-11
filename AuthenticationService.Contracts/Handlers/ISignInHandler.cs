using AuthenticationService.Contracts.Models;
using System.Threading.Tasks;

namespace AuthenticationService.Contracts.Handlers
{
    /// <summary>
    /// Authentication service.
    /// </summary>
    public interface ILoginHandler
    {
        /// <summary>
        /// Generate authentication token.
        /// </summary>
        Task<TokenModel> GetToken(LoginModel request);
    }
}
