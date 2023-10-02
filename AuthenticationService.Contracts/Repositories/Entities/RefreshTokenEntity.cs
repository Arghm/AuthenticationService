using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("refresh_tokens")]
    public class RefreshTokenEntity
    {
        [Column(name: "id", TypeName = "uuid"), Required]
        public Guid Id { get; set; }

        [Column(name: "user_id", TypeName = "uuid"), Required]
        public Guid UserId { get; set; }

        [Column(name: "token", TypeName = "text")]
        public string Token { get; set; }

        [Column(name: "expired_time", TypeName = "timestamptz"), Required]
        public DateTime Expired { get; set; }

        [Column(name: "created_time", TypeName = "timestamptz"), Required]
        public DateTime Created { get; set; }

        [Column(name: "is_blocked", TypeName = "bool")]
        public bool IsBlocked { get; set; }

        [Column(name: "jti", TypeName = "text")]
        public string Jti { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
