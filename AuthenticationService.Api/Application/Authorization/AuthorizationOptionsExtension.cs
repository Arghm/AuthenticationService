using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationService.Api.Application.Authorization
{
    public static class AuthorizationOptionsExtension
    {
        public static void AddPolicyWithSuperUser(this AuthorizationOptions options, string name, string claimType, string claimValue)
        {
            options.AddPolicy(name, c => c.RequireAssertion(context => context.User.IsInRole("Admin") || context.User.HasClaim(claimType, claimValue)));
        }
    }
}
