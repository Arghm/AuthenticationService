using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Contracts.Repositories;
using AuthenticationService.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AuthenticationService.Application.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AuthDbContext _dbContext;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(AuthDbContext dbContext, ILogger<RoleRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger ?? new NullLogger<RoleRepository>();
        }

        public async Task<IEnumerable<string>> GetAllRolesAsync()
        {
            var roles = await _dbContext.Roles.Select(c => c.Role).ToListAsync();

            return roles;
        }
    }
}
