using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthenticationService.Api.Application.Services.Jwt.Interfaces
{
    public interface IJwtService
    {
        (string accessToken, DateTime expiration) GenerateToken(string id, string userName, IEnumerable<Claim> userClaims);
        string RefreshAccessToken(string accessToken, string refreshToken);
        bool IsValidRefreshToken(string accessToken, string refreshToken);
    }
}
