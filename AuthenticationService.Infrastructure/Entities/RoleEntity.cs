using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Infrastructure.Entities
{
    [Table("Role")]
    public class RoleEntity
    {
        [Column(TypeName = "uniqueidentifier"), Required]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(255)"), Required]
        public string Role { get; set; }

        [Column(TypeName = "varchar(255)"), Required]
        public string NormalizeRole { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Description { get; set; }

        public virtual ICollection<UserRoleEntity> Users { get; set; }
    }
}
