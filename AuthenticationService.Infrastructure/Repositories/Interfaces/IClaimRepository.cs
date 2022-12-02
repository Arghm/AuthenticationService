using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationService.Infrastructure.Entities;

namespace AuthenticationService.Infrastructure.Repositories.Interfaces
{
    public interface IClaimRepository
    {
        Task<IEnumerable<Claim>> GetUserClaims(string id);
        Task<IEnumerable<ClaimEntity>> GetClaims();
    }
}
