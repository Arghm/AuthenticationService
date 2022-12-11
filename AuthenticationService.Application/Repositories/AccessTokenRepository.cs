using System;
using System.Threading.Tasks;
using AuthenticationService.Contracts.Repositories;
using AuthenticationService.Contracts.Repositories.Entities;
using AuthenticationService.Migrations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AuthenticationService.Application.Repositories
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
