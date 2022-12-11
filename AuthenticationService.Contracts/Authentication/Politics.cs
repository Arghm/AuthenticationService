using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Contracts.Authentication
{
    public static class Politics
    {
        public const string UserOperationsPolicy = "UserOperationsPolicy";
        public const string ReadOnlyUsersInfo = "OnlyReadUserInfo";
        public const string ReadWriteUsersInfo = "ReadWriteUsersInfo";
        public const string ReadWriteUsersClaims = "ReadWriteUsersClaims";
    }
}
