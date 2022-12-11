using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthenticationService.Contracts.Services
{
    public interface IJwtService
    {
        (string accessToken, DateTime expiration) GenerateToken(string id, string userName, IEnumerable<Claim> userClaims);
    }
}
