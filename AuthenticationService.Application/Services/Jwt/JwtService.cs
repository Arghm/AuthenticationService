using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using AuthenticationService.Contracts.Services;
using System.IdentityModel.Tokens.Jwt;
using AuthenticationService.Contracts.Models;

namespace AuthenticationService.Application.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public TokenModel GenerateToken(string id, string userName, IEnumerable<Claim> userClaims)
        {
            IEnumerable<Claim> accessTokenClaims = GetClaims(id, userName, userClaims);
            TokenModel accessToken = GenerateAccessToken(accessTokenClaims);

            return accessToken;
        }

        private TokenModel GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var expiration = DateTime.UtcNow.Add(_jwtOptions.AccessTokenExpiry);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.ValidIssuer,
                audience: _jwtOptions.ValidAudience,
                notBefore: DateTime.UtcNow,
                expires: expiration,
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_jwtOptions.AccessSecurityKeyBytes), SecurityAlgorithms.HmacSha256)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenModel { AccessToken = accessToken, Expiration = expiration };
        }

        private IEnumerable<Claim> GetClaims(string id, string userName, IEnumerable<Claim> userClaims = null)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, id),
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
            };
            if (userClaims != null)
                claims.AddRange(userClaims);

            return claims;
        }
    }

}
