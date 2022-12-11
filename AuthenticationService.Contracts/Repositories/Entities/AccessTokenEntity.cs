using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("AccessToken")]
    public class AccessTokenEntity
    {
        [Column(TypeName = "uniqueidentifier"), Required]
        public Guid Id { get; set; }

        [Column(TypeName = "uniqueidentifier"), Required]
        public Guid UserId { get; set; }

        [Column(TypeName = "varchar(1000)")]
        public string token { get; set; }

        [Column(TypeName = "datetime"), Required]
        public DateTime Expired { get; set; }

        [Column(TypeName = "datetime"), Required]
        public DateTime Created { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Jti { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string IpAddress { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
