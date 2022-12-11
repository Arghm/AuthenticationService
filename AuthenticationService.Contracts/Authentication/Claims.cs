using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Contracts.Authentication
{
    public static class Claims
    {
        public const string CreateUser = "CreateUser";
        public const string UpdateUser = "UpdateUser";
        public const string DeleteUser = "DeleteUser";
        public const string GetUsers = "GetUsers";
        public const string CreateClaim = "CreateClaim";
        public const string GetClaims = "GetClaims";
        public const string GetUserClaims = "GetUserClaims";
        public const string AddClaimsToUser = "AddClaimsToUser";
        public const string GetRoles = "GetRoles";
    }
}
