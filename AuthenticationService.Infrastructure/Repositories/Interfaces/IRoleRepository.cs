using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationService.Infrastructure.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<string>> GetAllRolesAsync();
    }
}
