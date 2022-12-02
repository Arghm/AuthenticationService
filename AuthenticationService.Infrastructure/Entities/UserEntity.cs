using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Infrastructure.Entities
{
    [Table("User")]
    public class UserEntity
    {
        [Column(TypeName = "uniqueidentifier"), Required]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(255)"), Required]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string NormalizedUserName { get; set; }

        [Column(TypeName = "varchar(255)"), Required]
        public string Password { get; set; }

        [Column(TypeName = "datetime"), Required]
        public DateTime Created { get; set; }

        public string[] IpAddresses { get; set; }

        [Column(TypeName = "bit"), Required]
        public bool IsActive { get; set; } = true;

        [Column(TypeName = "bit"), Required]
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<UserRoleEntity> Roles { get; set; }
        public virtual ICollection<UserClaimEntity> Claims { get; set; }
        public virtual ICollection<AccessTokenEntity> AccessTokens { get; set; }
        public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; }
    }
}
