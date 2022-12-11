using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("RefreshToken")]
    public class RefreshTokenEntity
    {
        [Column(TypeName = "uniqueidentifier"), Required]
        public Guid Id { get; set; }

        [Column(TypeName = "uniqueidentifier"), Required]
        public Guid UserId { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Token { get; set; }

        [Column(TypeName = "datetime"), Required]
        public DateTime Expired { get; set; }

        [Column(TypeName = "datetime"), Required]
        public DateTime Created { get; set; }

        [Column(TypeName = "bit")]
        public bool IsBlocked { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Jti { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
