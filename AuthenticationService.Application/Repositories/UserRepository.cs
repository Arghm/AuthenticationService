using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationService.Contracts.Models;
using AuthenticationService.Contracts.Repositories;
using AuthenticationService.Contracts.Repositories.Entities;
using AuthenticationService.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AuthenticationService.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(AuthDbContext dbContext, ILogger<UserRepository> logger = null)
        {
            _dbContext = dbContext;
            _logger = logger ?? new NullLogger<UserRepository>();
        }

        /// <inheritdoc/>
        public async Task<UserEntity> GetUserByUserName(string userName)
        {
            try
            {
                var user = await _dbContext
                    .Users
                    .AsNoTracking()
                    .AsSingleQuery()
                    .Include(c => c.Roles)
                    .ThenInclude(c => c.Role)
                    .Include(c => c.Claims)
                    .ThenInclude(c => c.Claim)
                    .SingleOrDefaultAsync(c => c.NormalizedUserName == userName.ToUpper());

                return user;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Get user by name error");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<UserEntity> GetUserByUserId(Guid userId)
        {
            try
            {
                var user = await _dbContext
                    .Users
                    .AsNoTracking()
                    .AsSingleQuery()
                    .Include(c => c.Roles)
                    .ThenInclude(c => c.Role)
                    .Include(c => c.Claims)
                    .ThenInclude(c => c.Claim)
                    .SingleOrDefaultAsync(c => c.Id == userId);

                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get user by name error");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<UserEntity[]> GetAllUsers()
        {
            try
            {
                return await _dbContext.Users.ToArrayAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get user error");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<int> CreateUser(UserEntity user)
        {
            try
            {
                _dbContext.Users.Add(user);
                return await _dbContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Create user error");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<int> UpdateUser(UserEntity user)
        {
            try
            {
                _dbContext.Users.Update(user);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Update user data error");
                throw;
            }
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
