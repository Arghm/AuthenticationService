namespace AuthenticationService.Api.Application.Authorization
{
    public static class Policies
    {
        public const string CreateUser = "create-user";
        public const string CreateClaim = "create-claim";
        public const string DeleteUser = "delete-user";
        public const string BlockUser = "block-user";
        public const string GetClaims = "get-claims";
        public const string GetUserClaims = "get-user-claims";
        public const string GetRoles = "get-roles";
        public const string GetUsers = "get-users";
        public const string AddClaimsToUser = "add-claims-to-user";
    }
}
