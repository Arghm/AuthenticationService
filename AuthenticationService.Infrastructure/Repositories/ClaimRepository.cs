using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using AuthenticationService.Infrastructure.Entities;
using AuthenticationService.Infrastructure.Repositories.Interfaces;

namespace AuthenticationService.Infrastructure.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly AuthDbContext _dbContext;
        private readonly ILogger<ClaimRepository> _logger;

        public ClaimRepository(AuthDbContext dbContext, ILogger<ClaimRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger ?? new NullLogger<ClaimRepository>();
        }

        public async Task<IEnumerable<Claim>> GetUserClaims(string id)
        {

            var result = await _dbContext.UserClaims
                .Include(c => c.Claim)
                .Where(c => c.UserId == new Guid(id))
                .Select(c => new Claim(c.Claim.Type, c.Claim.Value))
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<ClaimEntity>> GetClaims()
        {
            var result = await _dbContext.Claims
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
    }
}
