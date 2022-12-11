using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthenticationService.Contracts.Repositories.Entities;

namespace AuthenticationService.Contracts.Repositories.EntityConfigurations
{
    public class UserClaimEntityTypeConfiguration : IEntityTypeConfiguration<UserClaimEntity>
    {
        public void Configure(EntityTypeBuilder<UserClaimEntity> builder)
        {
            builder.ToTable("UserClaim");
            builder.HasKey(c => new { c.UserId, c.ClaimId });

            builder.HasOne(c => c.User).WithMany(c => c.Claims).HasForeignKey(c => c.UserId).IsRequired();
            builder.HasOne(c => c.Claim).WithMany(c => c.Users).HasForeignKey(c => c.ClaimId).IsRequired();
        }
    }
}
