using System;

namespace AuthenticationService.Api.Application.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        internal string Password { get; set; }
        internal string IpAddress { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
