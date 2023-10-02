using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("sign_in")]
    public class SignInEntity
    {
        [Column(name: "user_name", TypeName = "text"), Required]
        public string UserName { get; set; }

        [Column(name: "password", TypeName = "text"), Required]
        public string Password { get; set; }

        [Column(name: "ip_address", TypeName = "text"), MaxLength(20)]
        public string IpAddress { get; set; }

        public SignInEntity(string userName, string password, string ipAddress)
        {
            UserName = userName;
            Password = password;
            IpAddress = ipAddress;
        }

        public SignInEntity()
        {

        }
    }
}
