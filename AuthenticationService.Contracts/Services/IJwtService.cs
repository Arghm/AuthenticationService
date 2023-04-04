using AuthenticationService.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthenticationService.Contracts.Services
{
    public interface IJwtService
    {
        TokenModel GenerateToken(string id, string userName, IEnumerable<Claim> userClaims);
    }
}
