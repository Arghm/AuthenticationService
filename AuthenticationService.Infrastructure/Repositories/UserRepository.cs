using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using AuthenticationService.Infrastructure.Entities;
using AuthenticationService.Infrastructure.Repositories.Interfaces;

namespace AuthenticationService.Infrastructure.Repositories
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

        public async Task<UserEntity> GetUserByUserName(string userName)
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

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            var users = await _dbContext.Users.ToListAsync();

            return  users;
        }
    }
}
