using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Infrastructure.Entities
{
    [Table("Claim")]
    public class ClaimEntity
    {
        [Column(TypeName = "uniqueidentifier"), Required]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(255)"), Required]
        public string Type { get; set; }

        [Column(TypeName = "varchar(255)"), Required]
        public string Value { get; set; }


        [Column(TypeName = "varchar(255)"), Required]
        public string Issuer { get; set; }
        public virtual ICollection<UserClaimEntity> Users { get; set; }
    }
}