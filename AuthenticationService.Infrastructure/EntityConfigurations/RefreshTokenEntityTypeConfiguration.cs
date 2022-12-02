using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthenticationService.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Infrastructure.EntityConfigurations
{
    class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
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
