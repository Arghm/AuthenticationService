using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationService.Api.Application.Services.Jwt.Interfaces;

namespace AuthenticationService.Api.Application.Services.Jwt
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
            JwtSettings.Options = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            return serviceCollection.AddSingleton<IJwtService>(c => new JwtService(JwtSettings.Options));
        }

        public static IServiceCollection AddJwt(this IServiceCollection serviceCollection, Action<JwtOptions> options)
        {
            options.Invoke(JwtSettings.Options);
            serviceCollection.Configure<JwtOptions>(c => c = JwtSettings.Options);

            return serviceCollection.AddSingleton<IJwtService>(c => new JwtService(JwtSettings.Options));
        }
    }
}
