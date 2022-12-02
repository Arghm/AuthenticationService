using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthenticationService.Infrastructure.Entities;

namespace AuthenticationService.Infrastructure.EntityConfigurations
{
    public class AccessTokenEntityTypeConfiguration : IEntityTypeConfiguration<AccessTokenEntity>
    {
        public void Configure(EntityTypeBuilder<AccessTokenEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("AccessToken");
            builder
                .HasOne(c => c.User)
                .WithMany(c => c.AccessTokens)
                .HasForeignKey(c => c.UserId);
        }
    }
}
