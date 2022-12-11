using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Contracts.Repositories.Entities
{
    [Table("UserClaim")]
    public class UserClaimEntity
    {
        [Column(TypeName = "uniqueidentifier"), Required]
        public Guid UserId { get; set; }

        public UserEntity User { get; set; }

        [Column(TypeName = "uniqueidentifier"), Required]
        public Guid ClaimId { get; set; }

        public ClaimEntity Claim { get; set; }
    }
}
