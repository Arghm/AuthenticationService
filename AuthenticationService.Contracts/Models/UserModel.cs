using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Contracts.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }
}
