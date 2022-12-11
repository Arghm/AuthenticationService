using System;
using AuthenticationService.Contracts.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.Contracts.Repositories.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");

            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.NormalizedUserName).IsUnique();

            builder.Property(c => c.IpAddresses)
                .IsRequired()
                .HasConversion(c => string.Join(',', c), c => c.Split(',', StringSplitOptions.RemoveEmptyEntries)).IsRequired();
        }
    }
}
