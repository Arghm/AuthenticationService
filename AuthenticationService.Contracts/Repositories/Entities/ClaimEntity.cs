using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("claims")]
    public class ClaimEntity
    {
        [Column(name: "id", TypeName = "uuid"), Required]
        public Guid Id { get; set; }

        [Column(name: "claim_type", TypeName = "text"), Required]
        public string Type { get; set; }

        [Column(name: "claim_value", TypeName = "text"), Required]
        public string Value { get; set; }


        [Column(name: "issuer", TypeName = "text"), Required]
        public string Issuer { get; set; }
        public virtual ICollection<UserClaimEntity> Users { get; set; }
    }
}