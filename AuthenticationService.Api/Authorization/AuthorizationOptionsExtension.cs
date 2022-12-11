using AuthenticationService.Contracts.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;

namespace AuthenticationService.Api.Authorization
{
    public static class AuthorizationOptionsExtension
    {
        /// <summary>
        /// Add a new policy for claimType and automatically apply it for Role=Admin
        /// </summary>
        public static void AddPolicyWithSuperUser(this AuthorizationOptions options, string policyName, string claimType)
        {
            options.AddPolicy(policyName, p =>
                p.RequireAssertion(context =>
                    context.User.Claims.Any(a => a.Type == claimType)
                    || context.User.IsInRole(Roles.Admin)));
        }

        /// <summary>
        /// Add a new policy for claim and automatically apply it for Role=Admin
        /// </summary>
        public static void AddPolicyWithSuperUser(this AuthorizationOptions options, string policyName, string claimType, string claimName)
        {
            options.AddPolicy(policyName, policy =>
                policy.RequireAssertion(context => context.User.IsInRole(Roles.Admin) //context.User.HasClaim(ClaimTypes.Role, Claims.SuperUser)
                || context.User.HasClaim(claimType, claimName)));
        }

        /// <summary>
        /// Add a new policy for claims and automatically apply it for Role=Admin
        /// </summary>
        public static void AddPolicyWithSuperUser(this AuthorizationOptions options, string policyName, string claimType, params string[] claimsName)
        {
            options.AddPolicy(policyName, p =>
                p.RequireAssertion(context =>
                    context.User.Claims.Any(a => a.Type == claimType && claimsName.Contains(a.Value))
                    || context.User.IsInRole(Roles.Admin)));
        }
    }
}
