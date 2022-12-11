using AuthenticationService.Contracts.Models;
using System.Collections;
using System.Threading.Tasks;

namespace AuthenticationService.Contracts.Handlers
{
    /// <summary>
    /// User operations handler.
    /// </summary>
    public interface IUserHandler
    {
        /// <summary>
        /// Get all users info.
        /// </summary>
        Task<UserInfoModel[]> GetAllUsers();

        /// <summary>
        /// Get user info by user Id.
        /// </summary>
        Task<UserInfoModel> GetUserInfoByUserId(string userId);

        /// <summary>
        /// Get user info by user Name.
        /// </summary>
        Task<UserInfoModel> GetUserInfoByUserName(string userName);

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <returns>user Id</returns>
        Task<UserInfoModel> CreateUser(UserModel user);

        /// <summary>
        /// Update user data.
        /// </summary>
        Task<int> UpdateUser(UserUpdateModel user);

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> DeleteUser(string userId);
    }
}
