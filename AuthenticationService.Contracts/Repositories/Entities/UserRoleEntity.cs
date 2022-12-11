using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("UserRole")]
    public class UserRoleEntity
    {
        [Column(TypeName = "uniqueidentifier"), Required]
        public Guid UserId { get; set; }

        public UserEntity User { get; set; }

        [Column(TypeName = "uniqueidentifier"), Required]
        public Guid RoleId { get; set; }

        public RoleEntity Role { get; set; }
    }
}
