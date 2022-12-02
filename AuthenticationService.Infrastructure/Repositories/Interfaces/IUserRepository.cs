using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationService.Infrastructure.Entities;

namespace AuthenticationService.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserByUserName(string userName);
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
    }
}
