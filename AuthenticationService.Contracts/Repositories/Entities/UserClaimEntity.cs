using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("user_claims")]
    public class UserClaimEntity
    {
        [Column(name: "user_id", TypeName = "uuid"), Required]
        public Guid UserId { get; set; }

        public UserEntity User { get; set; }

        [Column(name: "claim_id", TypeName = "uuid"), Required]
        public Guid ClaimId { get; set; }

        public ClaimEntity Claim { get; set; }
    }
}
