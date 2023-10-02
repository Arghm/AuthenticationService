using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("roles")]
    public class RoleEntity
    {
        [Column(name: "id", TypeName = "uuid"), Required]
        public Guid Id { get; set; }

        [Column(name: "role_name", TypeName = "text"), Required]
        public string Role { get; set; }

        [Column(name: "normalized_role_name", TypeName = "text"), Required]
        public string NormalizeRole { get; set; }

        [Column(name: "role_description", TypeName = "text")]
        public string Description { get; set; }

        public virtual ICollection<UserRoleEntity> Users { get; set; }
    }
}
