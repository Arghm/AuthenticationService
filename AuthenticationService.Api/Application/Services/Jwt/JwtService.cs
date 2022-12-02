using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using AuthenticationService.Api.Application.Services.Jwt.Interfaces;

namespace AuthenticationService.Api.Application.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        // На будущее, может когда-нибудь понадобится рефреш токен. А пока обойдёмся только access
        //public (string AccessToken, string RefreshToken) GenerateTokens(string id, string userName, IEnumerable<Claim> userClaims)
        //{
        //    var accessTokenclaims = GetClaims(id, userName, userClaims);
        //    var refreshTokenClaims = GetClaims(id, userName);

        //    var accessToken = GenerateAccessToken(accessTokenclaims);
        //    var refreshToken = GenerateRefreshToken(refreshTokenClaims);

        //    return (accessToken, refreshToken);
        //}

        public (string accessToken, DateTime expiration) GenerateToken(string id, string userName, IEnumerable<Claim> userClaims)
        {
            var accessTokenClaims = GetClaims(id, userName, userClaims);
            var accessToken = GenerateAccessToken(accessTokenClaims);

            return (accessToken.token, accessToken.expiration);
        }

        public string RefreshAccessToken(string accessToken, string refreshToken)
        {
            Claim[] accessClaims = null;
            Claim[] refreshClaims = null;

            try
            {
                accessClaims = new JwtSecurityTokenHandler().ReadJwtToken(accessToken).Claims.ToArray();
                refreshClaims = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken).Claims.ToArray();
            }
            catch (Exception)
            {
                return null;
            }

            var refreshId = refreshClaims.FirstOrDefault(c => c.Type.Equals(JwtRegisteredClaimNames.Sub)).Value;
            var accessId = accessClaims.FirstOrDefault(c => c.Type.Equals(JwtRegisteredClaimNames.Sub)).Value;

            if (!refreshId.Equals(accessId))
                throw new UnauthorizedAccessException("Tokens mismatch");

            var userName = accessClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var customClaims = accessClaims
                .Where(aclaim => refreshClaims.All(rclaim => aclaim.Type != rclaim.Type));

            var claims = GetClaims(accessId, userName, customClaims);

            var newAccessToken = GenerateAccessToken(claims);

            return newAccessToken.token;
        }

        public bool IsValidRefreshToken(string accessToken, string refreshToken)
        {
            var jwt = new JwtSecurityTokenHandler();
            var refreshTokenParameters = GetRefreshTokenValidationParameters();
            var accessTokenParameters = GetAccessTokenValidationParametersToRefreshToken();

            try
            {
                jwt.ValidateToken(accessToken, accessTokenParameters, out var validateAccessToken);
                jwt.ValidateToken(refreshToken, refreshTokenParameters, out var validatedToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private (string token, DateTime expiration) GenerateAccessToken(IEnumerable<Claim> claims)
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

            return (accessToken, expiration);
        }

        private string GenerateRefreshToken(IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.ValidIssuer,
                audience: _jwtOptions.ValidAudience,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: DateTime.UtcNow.Add(_jwtOptions.RefreshTokenExpiry),
                signingCredentials:new SigningCredentials(new SymmetricSecurityKey(_jwtOptions.RefreshSecurityKeyBytes), SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
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

        private TokenValidationParameters GetRefreshTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.ValidIssuer,
                ValidAudience = _jwtOptions.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    _jwtOptions.RefreshSecurityKeyBytes)
            };
        }
        /// <summary>
        /// Валидируем access token при рефреше без лайфтайма
        /// </summary>
        /// <returns></returns>
        private TokenValidationParameters GetAccessTokenValidationParametersToRefreshToken()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.ValidIssuer,
                ValidAudience = _jwtOptions.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(
        _jwtOptions.AccessSecurityKeyBytes)
            };
        }
    }

}
