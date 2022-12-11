using AuthenticationService.Contracts.Repositories.Entities;
using AuthenticationService.Contracts.Repositories.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using AuthContracts = AuthenticationService.Contracts.Authentication;

namespace AuthenticationService.Migrations
{
    /// <summary>
    /// dotnet ef migrations add InitialCreate --startup-project AuthenticationService.Api --project AuthenticationService.Migrations
    /// dotnet ef database update --startup-project AuthenticationService.Api --project AuthenticationService.Migrations
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
            const string claimTypeUserData = ClaimTypes.UserData;
            const string claimIssuer = "AuthenticationService";

            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClaimsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccessTokenEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());

            //Users
            var adminId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            //Roles
            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();

            //Claims
            var createUserClaimId = Guid.NewGuid();
            var updateUserClaimId = Guid.NewGuid();
            var createClaimClaimId = Guid.NewGuid();
            var deleteUserClaimId = Guid.NewGuid();
            var getClaimsClaimId = Guid.NewGuid();
            var getUserClaimsClaimId = Guid.NewGuid();
            var getRolesClaimId = Guid.NewGuid();
            var getUsersClaimId = Guid.NewGuid();
            var addClaimsToUserClaimId = Guid.NewGuid();

            modelBuilder.Entity<RoleEntity>().HasData(new[]
            {
                new RoleEntity
                {
                    Id = adminRoleId,
                    Role = AuthContracts.Roles.Admin,
                    NormalizeRole = AuthContracts.Roles.Admin.ToUpper(),
                    Description = "Admin user"
                },
                new RoleEntity
                {
                    Id = userRoleId,
                    Role = AuthContracts.Roles.User,
                    NormalizeRole = AuthContracts.Roles.User.ToUpper(),
                    Description = "Base user"
                }
            });

            modelBuilder.Entity<ClaimEntity>().HasData(new[]
            {
                new ClaimEntity
                {
                    Id = createUserClaimId,
                    Type = claimTypeUserData,
                    Value = AuthContracts.Claims.CreateUser,
                    Issuer = claimIssuer,
                },
                new ClaimEntity
                {
                    Id = updateUserClaimId,
                    Type = claimTypeUserData,
                    Value = AuthContracts.Claims.UpdateUser,
                    Issuer = claimIssuer,
                },
                new ClaimEntity
                {
                    Id = createClaimClaimId,
                    Type = claimTypeUserData,
                    Value = AuthContracts.Claims.CreateClaim,
                    Issuer = claimIssuer
                },
                new ClaimEntity
                {
                    Id = deleteUserClaimId,
                    Type = claimTypeUserData,
                    Value = AuthContracts.Claims.DeleteUser,
                    Issuer = claimIssuer
                },
                new ClaimEntity
                {
                    Id = getClaimsClaimId,
                    Type = claimTypeUserData,
                    Value = AuthContracts.Claims.GetClaims,
                    Issuer = claimIssuer
                },
                new ClaimEntity
                {
                    Id = getUserClaimsClaimId,
                    Type = claimTypeUserData,
                    Value = AuthContracts.Claims.GetUserClaims,
                    Issuer = claimIssuer
                },
                new ClaimEntity
                {
                    Id = getRolesClaimId,
                    Type = claimTypeUserData,
                    Value = AuthContracts.Claims.GetRoles,
                    Issuer = claimIssuer
                },
                new ClaimEntity
                {
                    Id = getUsersClaimId,
                    Type = claimTypeUserData,
                    Value = AuthContracts.Claims.GetUsers,
                    Issuer = claimIssuer
                },
                new ClaimEntity
                {
                    Id = addClaimsToUserClaimId,
                    Type = claimTypeUserData,
                    Value = AuthContracts.Claims.AddClaimsToUser,
                    Issuer = claimIssuer
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
                    Password = "xsZQBit3Q2p+6szmN3kadwhNrbYgyLjI|100|0d//leOyX3Rab9ZEoez4Xv91FRyTlzYa", //pass 1
                    Created = DateTime.Now,
                },

                new UserEntity
                {
                    Id = userId,
                    UserName = "User1",
                    NormalizedUserName = "USER1",
                    IpAddresses = new[] {"0.0.0.1"},
                    Password = "mcaEigKpY/fMUDJ+guhfwihvkZFP8/1a|100|rVFQMn3mjsYonSzleF9Om9r3SFY/a+3J", //pass 1
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

            modelBuilder.Entity<UserClaimEntity>().HasData(new[]
            {
                new UserClaimEntity
                {
                    UserId = userId,
                    ClaimId = getUsersClaimId
                }
            });
        }
    }
}