using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace AuthenticationService.Application.Services.Jwt
{
    public class JwtOptions
    {
        public string ValidIssuer { get; set; } = "AuthenticationService";
        public string ValidAudience { get; set; } = "AuthenticationService";
        public string AccessSecurityKey { get; set; } = "DevAccessSecurityKey";
        public string RefreshSecurityKey { get; set; } = "DevRefreshKey";
        public TimeSpan AccessTokenExpiry { get; set; } = TimeSpan.FromMinutes(30);
        public TimeSpan RefreshTokenExpiry { get; set; } = TimeSpan.FromDays(5);
        public TimeSpan UpdateRefreshTokenBeforeExpired { get; set; } = TimeSpan.FromDays(1);
        public byte[] AccessSecurityKeyBytes => Encoding.UTF8.GetBytes(AccessSecurityKey);
        public byte[] RefreshSecurityKeyBytes => Encoding.UTF8.GetBytes(RefreshSecurityKey);

        public TokenValidationParameters GetAccessTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = ValidIssuer,
                ValidAudience = ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(AccessSecurityKeyBytes),
                ClockSkew = TimeSpan.Zero
            };
        }
    }

    public static class JwtSettings
    {
        internal static JwtOptions Options { get; set; } = new JwtOptions();

        public static TokenValidationParameters TokenValidationParameters()
        {
            return Options == null 
                ? new JwtOptions().GetAccessTokenValidationParameters() 
                : Options.GetAccessTokenValidationParameters();
        }
    }
}
