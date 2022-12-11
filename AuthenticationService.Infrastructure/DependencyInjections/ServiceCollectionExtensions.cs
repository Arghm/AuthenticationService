using AuthenticationService.Application.Handlers;
using AuthenticationService.Application.Repositories;
using AuthenticationService.Contracts.Handlers;
using AuthenticationService.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService.Infrastructure.DependencyInjections
{
    /// <summary>
    /// IServiceCollection extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configure custom services.
        /// </summary>
        public static IServiceCollection AddAuthServices(this IServiceCollection services)
        {
            // services
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            // repositories
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IClaimRepository, ClaimRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccessTokenRepository, AccessTokenRepository>();
            services.AddScoped<ILoginHandler, LoginHandler>();
            services.AddScoped<IUserHandler, UserHandler>();

            return services;
        }
    }
}
