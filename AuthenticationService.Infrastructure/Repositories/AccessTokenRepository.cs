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
    public class AccessTokenRepository : IAccessTokenRepository
    {
        private readonly AuthDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;
        public AccessTokenRepository(AuthDbContext dbContext, ILogger<UserRepository> logger = null)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? new NullLogger<UserRepository>();
        }

        public async Task<string> CreateAccessTokenAsync(AccessTokenEntity accessToken)
        {
            var id = Guid.NewGuid();

            await _dbContext.AccessTokens.AddAsync(new AccessTokenEntity
            {
                Id = id,
                Created = DateTime.UtcNow,
                token = accessToken.token,
                IpAddress = accessToken.IpAddress,
                Jti = accessToken.Jti,
                User = accessToken.User,
                UserId = accessToken.UserId,
                Expired = accessToken.Expired
            });

            await _dbContext.SaveChangesAsync();

            return id.ToString();
        }



    }
}
