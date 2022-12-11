using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationService.Contracts.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<string>> GetAllRolesAsync();
    }
}
