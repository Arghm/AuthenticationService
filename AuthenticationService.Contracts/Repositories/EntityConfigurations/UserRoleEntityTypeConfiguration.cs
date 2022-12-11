using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthenticationService.Contracts.Repositories.Entities;

namespace AuthenticationService.Contracts.Repositories.EntityConfigurations
{
    public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.ToTable("UserRole");

            builder.HasKey(c => new {c.UserId, c.RoleId});

            builder.HasOne(c => c.User).WithMany(c => c.Roles).HasForeignKey(c => c.UserId).IsRequired();
            builder.HasOne(c => c.Role).WithMany(c => c.Users).HasForeignKey(c => c.RoleId).IsRequired();
        }
    }
}
