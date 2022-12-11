using System.Threading.Tasks;
using AuthenticationService.Contracts.Repositories.Entities;

namespace AuthenticationService.Contracts.Repositories
{
    public interface IAccessTokenRepository
    {
        Task<string> CreateAccessTokenAsync(AccessTokenEntity accessToken);
    }
}
