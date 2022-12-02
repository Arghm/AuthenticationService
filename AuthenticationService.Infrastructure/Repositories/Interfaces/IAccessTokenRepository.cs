using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationService.Infrastructure.Entities;

namespace AuthenticationService.Infrastructure.Repositories.Interfaces
{
    public interface IAccessTokenRepository
    {
        Task<string> CreateAccessTokenAsync(AccessTokenEntity accessToken);
    }
}
