using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationService.Contracts.Repositories.Entities;

namespace AuthenticationService.Contracts.Repositories
{
    /// <summary>
    /// User repository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get user by UserName.
        /// </summary>
        /// <returns>User data with roles and claims</returns>
        Task<UserEntity> GetUserByUserName(string userName);

        /// <summary>
        /// Get user by Id.
        /// </summary>
        /// <returns>User data with roles and claims</returns>
        Task<UserEntity> GetUserByUserId(Guid userId);

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <returns>user id</returns>
        Task<int> CreateUser(UserEntity user);

        /// <summary>
        /// Update user data.
        /// </summary>
        /// <returns>user id</returns>
        Task<int> UpdateUser(UserEntity user);

        /// <summary>
        /// Get all users info
        /// </summary>
        Task<UserEntity[]> GetAllUsers();
    }
}
