using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("SignIn")]
    public class SignInEntity
    {
        [Column(TypeName = "varchar(255)"), Required]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(255)"), Required]
        public string Password { get; set; }

        [Column(TypeName = "varchar(255)"), MaxLength(20)]
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
