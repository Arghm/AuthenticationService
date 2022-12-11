using AuthenticationService.Contracts.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.Contracts.Repositories.EntityConfigurations
{
    public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.ToTable("RefreshToken");

            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Jti).IsUnique();

            builder.HasOne(c => c.User)
                .WithMany(c => c.RefreshTokens)
                .HasForeignKey(c => c.UserId);
        }
    }
}
