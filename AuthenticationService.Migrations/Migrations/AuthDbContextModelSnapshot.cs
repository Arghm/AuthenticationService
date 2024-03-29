﻿// <auto-generated />
using System;
using AuthenticationService.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuthenticationService.Migrations.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    partial class AuthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.AccessTokenEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("created_time")
                        .HasColumnType("timestamptz");

                    b.Property<DateTime>("expired_time")
                        .HasColumnType("timestamptz");

                    b.Property<string>("ip_address")
                        .HasColumnType("text");

                    b.Property<string>("jti")
                        .HasColumnType("text");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.Property<string>("token")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.ToTable("access_tokens");
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.ClaimEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("issuer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("claim_type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("claim_value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("claim");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2276e40d-bae0-4b78-8968-784ffd3b91a0"),
                            Issuer = "AuthenticationService",
                            Type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata",
                            Value = "CreateUser"
                        },
                        new
                        {
                            Id = new Guid("9a8aea34-96f6-4f66-9712-bb6af72298c8"),
                            Issuer = "AuthenticationService",
                            Type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata",
                            Value = "UpdateUser"
                        },
                        new
                        {
                            Id = new Guid("6175c84d-143a-493c-91ac-ee7bcce18bd9"),
                            Issuer = "AuthenticationService",
                            Type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata",
                            Value = "CreateClaim"
                        },
                        new
                        {
                            Id = new Guid("d9df1ee5-614d-4c5f-97ec-96edd32e6818"),
                            Issuer = "AuthenticationService",
                            Type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata",
                            Value = "DeleteUser"
                        },
                        new
                        {
                            Id = new Guid("6ef5f8cc-cfa9-47d8-834d-c6163c44cc54"),
                            Issuer = "AuthenticationService",
                            Type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata",
                            Value = "GetClaims"
                        },
                        new
                        {
                            Id = new Guid("1fe742d8-8b46-4e86-8d37-57b613105f9e"),
                            Issuer = "AuthenticationService",
                            Type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata",
                            Value = "GetUserClaims"
                        },
                        new
                        {
                            Id = new Guid("4609c1ce-a9f6-4c9e-ad06-407fb78dfe7b"),
                            Issuer = "AuthenticationService",
                            Type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata",
                            Value = "GetRoles"
                        },
                        new
                        {
                            Id = new Guid("9dd0342f-d827-4d82-915d-cef218c58c6f"),
                            Issuer = "AuthenticationService",
                            Type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata",
                            Value = "GetUsers"
                        },
                        new
                        {
                            Id = new Guid("b752b927-fc95-447a-9256-fd737804960c"),
                            Issuer = "AuthenticationService",
                            Type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata",
                            Value = "AddClaimsToUser"
                        });
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.RefreshTokenEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("created_time")
                        .HasColumnType("timestamptz");

                    b.Property<DateTime>("expired_time")
                        .HasColumnType("timestamptz");

                    b.Property<bool>("is_blocked")
                        .HasColumnType("bool");

                    b.Property<string>("jti")
                        .HasColumnType("text");

                    b.Property<string>("token")
                        .HasColumnType("text");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.HasIndex("jti")
                        .IsUnique()
                        .HasFilter("[Jti] IS NOT NULL");

                    b.HasIndex("user_id");

                    b.ToTable("refresh_tokens");
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("role_description")
                        .HasColumnType("text");

                    b.Property<string>("normalized_role_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("role_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("normalized_role_name")
                        .IsUnique();

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a404953c-d47b-4075-9027-0a4350f52160"),
                            Description = "Admin user",
                            NormalizeRole = "ADMIN",
                            Role = "Admin"
                        },
                        new
                        {
                            Id = new Guid("b582e5d0-6f55-4b2e-82a4-d9c4c58cb54a"),
                            Description = "Base user",
                            NormalizeRole = "USER",
                            Role = "User"
                        });
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.UserClaimEntity", b =>
                {
                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("claim_id")
                        .HasColumnType("uuid");

                    b.HasKey("user_id", "claim_id");

                    b.HasIndex("claim_id");

                    b.ToTable("user_claims");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("9ffc9b38-1ead-4373-a913-718f67165c3b"),
                            ClaimId = new Guid("9dd0342f-d827-4d82-915d-cef218c58c6f")
                        });
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("created_time")
                        .HasColumnType("timestamptz");

                    b.Property<string>("ip_addresses")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("is_active")
                        .HasColumnType("bool");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("bool");

                    b.Property<string>("normalized_user_name")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("normalized_user_name")
                        .IsUnique()
                        .HasFilter("[normalized_user_name] IS NOT NULL");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e1a056cb-4854-43bd-baba-28fb6f3a8b4d"),
                            Created = new DateTime(2022, 12, 11, 21, 36, 30, 105, DateTimeKind.Local).AddTicks(9278),
                            IpAddresses = "0.0.0.1",
                            IsActive = true,
                            IsDeleted = false,
                            NormalizedUserName = "SUPERUSER",
                            Password = "xsZQBit3Q2p+6szmN3kadwhNrbYgyLjI|100|0d//leOyX3Rab9ZEoez4Xv91FRyTlzYa",
                            UserName = "SuperUser"
                        },
                        new
                        {
                            Id = new Guid("9ffc9b38-1ead-4373-a913-718f67165c3b"),
                            Created = new DateTime(2022, 12, 11, 21, 36, 30, 106, DateTimeKind.Local).AddTicks(9039),
                            IpAddresses = "0.0.0.1",
                            IsActive = true,
                            IsDeleted = false,
                            NormalizedUserName = "USER1",
                            Password = "mcaEigKpY/fMUDJ+guhfwihvkZFP8/1a|100|rVFQMn3mjsYonSzleF9Om9r3SFY/a+3J",
                            UserName = "User1"
                        });
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.UserRoleEntity", b =>
                {
                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("role_id")
                        .HasColumnType("uuid");

                    b.HasKey("user_id", "role_id");

                    b.HasIndex("role_id");

                    b.ToTable("user_roles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("e1a056cb-4854-43bd-baba-28fb6f3a8b4d"),
                            RoleId = new Guid("a404953c-d47b-4075-9027-0a4350f52160")
                        });
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.AccessTokenEntity", b =>
                {
                    b.HasOne("AuthenticationService.Contracts.Repositories.Entities.UserEntity", "users")
                        .WithMany("access_tokens")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("users");
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.RefreshTokenEntity", b =>
                {
                    b.HasOne("AuthenticationService.Contracts.Repositories.Entities.UserEntity", "users")
                        .WithMany("refresh_tokens")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("users");
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.UserClaimEntity", b =>
                {
                    b.HasOne("AuthenticationService.Contracts.Repositories.Entities.ClaimEntity", "claims")
                        .WithMany("users")
                        .HasForeignKey("claim_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthenticationService.Contracts.Repositories.Entities.UserEntity", "users")
                        .WithMany("claims")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("claims");

                    b.Navigation("users");
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.UserRoleEntity", b =>
                {
                    b.HasOne("AuthenticationService.Contracts.Repositories.Entities.RoleEntity", "roles")
                        .WithMany("users")
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthenticationService.Contracts.Repositories.Entities.UserEntity", "users")
                        .WithMany("roles")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("roles");

                    b.Navigation("users");
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.ClaimEntity", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.RoleEntity", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("AuthenticationService.Contracts.Repositories.Entities.UserEntity", b =>
                {
                    b.Navigation("access_tokens");

                    b.Navigation("claims");

                    b.Navigation("refresh_tokens");

                    b.Navigation("roles");
                });
#pragma warning restore 612, 618
        }
    }
}
