using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("access_tokens")]
    public class AccessTokenEntity
    {
        [Column(name: "id", TypeName = "uuid"), Required]
        public Guid Id { get; set; }

        [Column(name: "user_id", TypeName = "uuid"), Required]
        public Guid UserId { get; set; }

        [Column(name: "token", TypeName = "text")]
        public string token { get; set; }

        [Column(name: "expired_time", TypeName = "timestamptz"), Required]
        public DateTime Expired { get; set; }

        [Column(name: "created_time", TypeName = "timestamptz"), Required]
        public DateTime Created { get; set; }

        [Column(name: "jti", TypeName = "text")]
        public string Jti { get; set; }

        [Column(name: "ip_address", TypeName = "varchar(20)")]
        public string IpAddress { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
