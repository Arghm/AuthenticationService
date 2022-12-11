using AuthenticationService.Contracts.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.Contracts.Repositories.EntityConfigurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.NormalizeRole).IsUnique();
        }
    }
}
