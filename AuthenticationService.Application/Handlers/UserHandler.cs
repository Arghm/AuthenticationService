using AuthenticationService.Contracts.Exceptions;
using AuthenticationService.Contracts.Handlers;
using AuthenticationService.Contracts.Models;
using AuthenticationService.Contracts.Repositories;
using AuthenticationService.Contracts.Repositories.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Handlers
{
    /// <inheritdoc/>
    public class UserHandler : IUserHandler
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UserHandler(IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ILogger<UserHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _logger = logger ?? new NullLogger<UserHandler>();
        }
        /// <inheritdoc/>
        public async Task<UserInfoModel> CreateUser(UserModel user)
        {
            ValidateUserModel(user);

            var existUser = await _userRepository.GetUserByUserName(user.UserName);

            if (existUser == null)
            {
                var userEntity = MapUserModelToEntity(user);
                await _userRepository.CreateUser(userEntity);

                return new UserInfoModel
                {
                    Id = userEntity.Id,
                    Created = userEntity.Created,
                };
            }
            else
            {
                throw new BadRequestException($"user already exists: id = {existUser.Id}");
            }
        }

        /// <inheritdoc/>
        public Task<int> DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<UserInfoModel[]> GetAllUsers()
        {
            var usersEntities = await _userRepository.GetAllUsers();
            return usersEntities.Select(MapUserEntityToInfoModel).ToArray();
        }

        /// <inheritdoc/>
        public async Task<UserInfoModel> GetUserInfoByUserId(string userId)
        {
            if (Guid.TryParse(userId, out Guid userIdGuid))
            {
                var result = await _userRepository.GetUserByUserId(userIdGuid);
                if (result != null)
                {
                    return MapUserEntityToInfoModel(result);
                }
                return null;
            }
            return null;
        }

        /// <inheritdoc/>
        public async Task<UserInfoModel> GetUserInfoByUserName(string userName)
        {
            var result = await _userRepository.GetUserByUserName(userName);
            if (result != null)
            {
                return MapUserEntityToInfoModel(result);
            }
            return null;
        }

        /// <inheritdoc/>
        public async Task<int> UpdateUser(UserUpdateModel user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            UserEntity existUser = null;

            if (user.Id != null)
            {
                existUser = await _userRepository.GetUserByUserId(user.Id);
            }
            else if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                existUser = await _userRepository.GetUserByUserName(user.UserName);
            }

            if (existUser != null)
            {
                var userEntity = MapUserInfoModelToEntityForUpdate(user, existUser);
                return await _userRepository.UpdateUser(userEntity);
            }
            else
            {
                throw new BadRequestException("User not found");
            }
        }

        private void ValidateUserModel(UserModel user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));
            if(string.IsNullOrWhiteSpace(user.UserName))
                throw new ArgumentNullException(nameof(user.UserName));
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentNullException(nameof(user.Password));
        }

        private UserEntity MapUserModelToEntity(UserModel user)
        {
            var userIpAddresses = user.IpAddress != null ? user.IpAddress.Split(',') : null;
            return new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = user.UserName,
                Password = _passwordHasher.Hash(user.Password),
                IpAddresses = userIpAddresses,
                NormalizedUserName = user.UserName.ToUpper(),
                Created = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
            };
        }

        private UserEntity MapUserInfoModelToEntityForUpdate(UserUpdateModel user, UserEntity currentUser)
        {
            return new UserEntity
            {
                Password = !String.IsNullOrWhiteSpace(user.Password) ? _passwordHasher.Hash(user.Password) : currentUser.Password,
                IpAddresses = user.IpAddresses != null ? user.IpAddresses : currentUser.IpAddresses,
                UserName = !string.IsNullOrEmpty(user.UserName) ? user.UserName : currentUser.UserName,
                NormalizedUserName = !string.IsNullOrEmpty(user.UserName) ? user.UserName.ToUpper() : currentUser.NormalizedUserName,
                IsActive = user.IsActive != null ? (bool)user.IsActive : currentUser.IsActive,
                IsDeleted = user.IsDeleted != null ? (bool)user.IsDeleted : currentUser.IsDeleted,
                Created = currentUser.Created,
                AccessTokens = currentUser.AccessTokens,
                Claims = currentUser.Claims,
                RefreshTokens = currentUser.RefreshTokens,
                Roles = currentUser.Roles,
                Id = currentUser.Id,
            };
        }

        private UserInfoModel MapUserEntityToInfoModel(UserEntity userEntity)
        {
            return new UserInfoModel
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                Created = userEntity.Created,
                IpAddresses = userEntity.IpAddresses,
                IsActive = userEntity.IsActive,
                IsDeleted = userEntity.IsDeleted,
            };
        }
    }
}
