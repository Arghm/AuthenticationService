using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("users")]
    public class UserEntity
    {
        [Column(name: "id", TypeName = "uuid"), Required]
        public Guid Id { get; set; }

        [Column(name: "user_name", TypeName = "text"), Required]
        public string UserName { get; set; }

        [Column(name: "normalized_user_name", TypeName = "text")]
        public string NormalizedUserName { get; set; }

        [Column(name: "password", TypeName = "text"), Required]
        public string Password { get; set; }

        [Column(name: "created_time", TypeName = "timestamptz"), Required]
        public DateTime Created { get; set; }

        public string[] IpAddresses { get; set; }

        [Column(name: "is_active", TypeName = "bool"), Required]
        public bool IsActive { get; set; } = true;

        [Column(name: "is_deleted", TypeName = "bool"), Required]
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<UserRoleEntity> Roles { get; set; }
        public virtual ICollection<UserClaimEntity> Claims { get; set; }
        public virtual ICollection<AccessTokenEntity> AccessTokens { get; set; }
        public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; }
    }
}
