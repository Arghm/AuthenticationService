using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthenticationService.Infrastructure.Entities;

namespace AuthenticationService.Infrastructure.EntityConfigurations
{
    public class ClaimsEntityTypeConfiguration : IEntityTypeConfiguration<ClaimEntity>
    {
        public void Configure(EntityTypeBuilder<ClaimEntity> builder)
        {
            builder.ToTable("Claim");

            builder.HasKey(c => c.Id);
        }
    }
}
