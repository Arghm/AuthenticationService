using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationService.Contracts.Repositories.Entities;

namespace AuthenticationService.Contracts.Repositories
{
    public interface IClaimRepository
    {
        Task<IEnumerable<Claim>> GetUserClaims(string id);
        Task<IEnumerable<ClaimEntity>> GetClaims();
    }
}
