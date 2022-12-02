using AuthenticationService.Infrastructure.Entities;
using AuthenticationService.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Infrastructure
{
    /// <summary>
    /// dotnet ef migrations add InitialCreate --startup-project AuthenticationService.Api --project AuthenticationService.Infrastructure
    /// dotnet ef database update --startup-project AuthenticationService.Api --project AuthenticationService.Infrastructure
    /// </summary>
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<ClaimEntity> Claims { get; set; }
        public DbSet<UserClaimEntity> UserClaims { get; set; }
        public DbSet<AccessTokenEntity> AccessTokens { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClaimsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccessTokenEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());

            //Admin
            var adminId = new Guid("dee05e65-6d95-431f-b953-5f84c31bde8c");

            //Roles
            var adminRoleId = new Guid("dee05e65-6d95-431f-b953-5f84c31be546");
            var userRoleId = new Guid("32a24807-eda2-4f86-88ca-0a6264706994");

            //Claims
            var createUserClaimId = new Guid("dee05e65-6d95-431f-b953-5f84c31bdb8a");
            var createClaimClaimId = new Guid("0f47684b-731d-4cf2-af24-6fc25ee28afe");
            var deleteUserClaimId = new Guid("69a79790-c73a-4e9c-b130-22384187b5f1");
            var blockUserClaimId = new Guid("ef0b9bff-5634-44b0-9610-b887788f80a4");
            var getClaimsClaimId = new Guid("8b70ae03-a428-46c1-8e26-daed1cebd841");
            var getUserClaimsClaimId = new Guid("ce9be9f4-05ce-4763-8087-16222efe65e8");
            var getRolesClaimId = new Guid("68ac2926-0ae0-400e-8206-0097875cc414");
            var getUsersClaimId = new Guid("dcf5116b-20e2-477d-8086-166a49201ada");
            var addClaimsToUserClaimId = new Guid("76842170-e73a-4f2b-9aaf-0351ecb0d862");

            modelBuilder.Entity<RoleEntity>().HasData(new[]
            {
                new RoleEntity
                {
                    Id = adminRoleId,
                    Role = "Admin",
                    NormalizeRole = "ADMIN",
                    Description = "Admin user"
                },
                new RoleEntity
                {
                    Id = userRoleId,
                    Role = "User",
                    NormalizeRole = "USER",
                    Description = "Base user"
                }
            });

            modelBuilder.Entity<ClaimEntity>().HasData(new[]
            {
                new ClaimEntity
                {
                    Id = createUserClaimId,
                    Type = "AuthenticationService",
                    Value = "CreateUser",
                    Issuer = "Authentication service"
                },
                new ClaimEntity
                {
                    Id = createClaimClaimId,
                    Type = "AuthenticationService",
                    Value = "CreateClaim",
                    Issuer = "Authentication service"
                },
                new ClaimEntity
                {
                    Id = deleteUserClaimId,
                    Type = "AuthenticationService",
                    Value = "DeleteUser",
                    Issuer = "Authentication service"
                },
                new ClaimEntity
                {
                    Id = blockUserClaimId,
                    Type = "AuthenticationService",
                    Value = "BlockUser",
                    Issuer = "Authentication service"
                },
                new ClaimEntity
                {
                    Id = getClaimsClaimId,
                    Type = "AuthenticationService",
                    Value = "GetClaims",
                    Issuer = "Authentication service"
                },
                new ClaimEntity
                {
                    Id = getUserClaimsClaimId,
                    Type = "AuthenticationService",
                    Value = "GetUserClaims",
                    Issuer = "Authentication service"
                },
                new ClaimEntity
                {
                    Id = getRolesClaimId,
                    Type = "AuthenticationService",
                    Value = "GetRoles",
                    Issuer = "Authentication service"
                },
                new ClaimEntity
                {
                    Id = getUsersClaimId,
                    Type = "AuthenticationService",
                    Value = "GetUsers",
                    Issuer = "Authentication service"
                },
                new ClaimEntity
                {
                    Id = addClaimsToUserClaimId,
                    Type = "AuthenticationService",
                    Value = "AddClaimsToUser",
                    Issuer = "Authentication service"
                },
            });

            modelBuilder.Entity<UserEntity>().HasData(new[]
            {
                new UserEntity
                {
                    Id = adminId,
                    UserName = "SuperUser",
                    NormalizedUserName = "SUPERUSER",
                    IpAddresses = new[] {"0.0.0.1"},
                    Password = "1",
                    Created = DateTime.Now,
                },
            });

            modelBuilder.Entity<UserRoleEntity>().HasData(new[]
            {
                new UserRoleEntity
                {
                    UserId = adminId,
                    RoleId = adminRoleId
                }
            });
        }
    }
}