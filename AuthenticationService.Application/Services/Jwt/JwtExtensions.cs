using System;
using AuthenticationService.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService.Application.Services.Jwt
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection serviceCollection, Action<JwtOptions> options)
        {
            options.Invoke(JwtSettings.Options);
            serviceCollection.Configure<JwtOptions>(c => c = JwtSettings.Options);

            return serviceCollection.AddSingleton<IJwtService>(c => new JwtService(JwtSettings.Options));
        }
    }
}
