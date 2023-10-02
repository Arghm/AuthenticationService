using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("user_roles")]
    public class UserRoleEntity
    {
        [Column(name: "user_id", TypeName = "uuid"), Required]
        public Guid UserId { get; set; }

        public UserEntity User { get; set; }

        [Column(name: "roles_id", TypeName = "uuid"), Required]
        public Guid RoleId { get; set; }

        public RoleEntity Role { get; set; }
    }
}
