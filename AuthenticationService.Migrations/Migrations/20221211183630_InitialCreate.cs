using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthenticationService.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "сlaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(name: "id", type: "uuid", nullable: false),
                    Type = table.Column<string>(name: "claim_type", type: "text", nullable: false),
                    Value = table.Column<string>(name: "claim_value", type: "text", nullable: false),
                    Issuer = table.Column<string>(name: "issuer", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_claim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(name: "id", type: "uuid", nullable: false),
                    Role = table.Column<string>(name: "role_name", type: "text", nullable: false),
                    NormalizeRole = table.Column<string>(name: "normalized_role_name", type: "text", nullable: false),
                    Description = table.Column<string>(name: "role_description", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(name: "id", type: "uuid", nullable: false),
                    UserName = table.Column<string>(name: "user_name", type: "text", nullable: false),
                    NormalizedUserName = table.Column<string>(name: "normalized_user_name", type: "text", nullable: true),
                    Password = table.Column<string>(name: "password", type: "text", nullable: false),
                    Created = table.Column<DateTime>(name: "created_time", type: "timestamptz", nullable: false),
                    IpAddresses = table.Column<string>(name: "ip_address", type: "text", nullable: false),
                    IsActive = table.Column<bool>(name: "is_active", type: "bool", nullable: false),
                    IsDeleted = table.Column<bool>(name: "is_deleted", type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "access_tokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(name: "id", type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    token = table.Column<string>(name: "token", type: "text", nullable: true),
                    Expired = table.Column<DateTime>(name: "expired_time", type: "timestamptz", nullable: false),
                    Created = table.Column<DateTime>(name: "created_time", type: "timestamptz", nullable: false),
                    Jti = table.Column<string>(name: "jti", type: "text", nullable: true),
                    IpAddress = table.Column<string>(name: "ip_address", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "fk_access_tokens_users_user_id",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(name: "id", type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    Token = table.Column<string>(name: "token", type: "text", nullable: true),
                    Expired = table.Column<DateTime>(name: "expired_time", type: "timestamptz", nullable: false),
                    Created = table.Column<DateTime>(name: "created_time", type: "timestamptz", nullable: false),
                    IsBlocked = table.Column<bool>(name: "is_blocked", type: "bool", nullable: false),
                    Jti = table.Column<string>(name: "jti", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "fk_refresh_tokens_users_user_id",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    UserId = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    ClaimId = table.Column<Guid>(name: "claim_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claims", x => new { x.UserId, x.ClaimId });
                    table.ForeignKey(
                        name: "fk_user_claims_claims_claim_id",
                        column: x => x.ClaimId,
                        principalTable: "claims",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_claims_users_user_id",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(name: "role_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "fk_user_roles_role_role_id",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "claims",
                columns: new[] { "id", "Issuer", "claim_type", "claim_value" },
                values: new object[,]
                {
                    { new Guid("2276e40d-bae0-4b78-8968-784ffd3b91a0"), "AuthenticationService", "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", "CreateUser" },
                    { new Guid("9a8aea34-96f6-4f66-9712-bb6af72298c8"), "AuthenticationService", "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", "UpdateUser" },
                    { new Guid("6175c84d-143a-493c-91ac-ee7bcce18bd9"), "AuthenticationService", "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", "CreateClaim" },
                    { new Guid("d9df1ee5-614d-4c5f-97ec-96edd32e6818"), "AuthenticationService", "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", "DeleteUser" },
                    { new Guid("6ef5f8cc-cfa9-47d8-834d-c6163c44cc54"), "AuthenticationService", "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", "GetClaims" },
                    { new Guid("1fe742d8-8b46-4e86-8d37-57b613105f9e"), "AuthenticationService", "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", "GetUserClaims" },
                    { new Guid("4609c1ce-a9f6-4c9e-ad06-407fb78dfe7b"), "AuthenticationService", "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", "GetRoles" },
                    { new Guid("9dd0342f-d827-4d82-915d-cef218c58c6f"), "AuthenticationService", "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", "GetUsers" },
                    { new Guid("b752b927-fc95-447a-9256-fd737804960c"), "AuthenticationService", "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", "AddClaimsToUser" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "role_description", "normalized_role_name", "role_name" },
                values: new object[,]
                {
                    { new Guid("a404953c-d47b-4075-9027-0a4350f52160"), "Admin user", "ADMIN", "Admin" },
                    { new Guid("b582e5d0-6f55-4b2e-82a4-d9c4c58cb54a"), "Base user", "USER", "User" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "created_time", "ip_addresses", "is_active", "is_deleted", "normalized_user_name", "password", "user_name" },
                values: new object[,]
                {
                    { new Guid("e1a056cb-4854-43bd-baba-28fb6f3a8b4d"), new DateTime(2022, 12, 11, 21, 36, 30, 105, DateTimeKind.Local).AddTicks(9278), "0.0.0.1", true, false, "SUPERUSER", "xsZQBit3Q2p+6szmN3kadwhNrbYgyLjI|100|0d//leOyX3Rab9ZEoez4Xv91FRyTlzYa", "SuperUser" },
                    { new Guid("9ffc9b38-1ead-4373-a913-718f67165c3b"), new DateTime(2022, 12, 11, 21, 36, 30, 106, DateTimeKind.Local).AddTicks(9039), "0.0.0.1", true, false, "USER1", "mcaEigKpY/fMUDJ+guhfwihvkZFP8/1a|100|rVFQMn3mjsYonSzleF9Om9r3SFY/a+3J", "User1" }
                });

            migrationBuilder.InsertData(
                table: "user_claims",
                columns: new[] { "claim_id", "user_id" },
                values: new object[] { new Guid("9dd0342f-d827-4d82-915d-cef218c58c6f"), new Guid("9ffc9b38-1ead-4373-a913-718f67165c3b") });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { new Guid("a404953c-d47b-4075-9027-0a4350f52160"), new Guid("e1a056cb-4854-43bd-baba-28fb6f3a8b4d") });

            migrationBuilder.CreateIndex(
                name: "ix_access_tokens_user_id",
                table: "access_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_jti",
                table: "refresh_tokens",
                column: "jti",
                unique: true,
                filter: "[jti] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_normalized_role_name",
                table: "roles",
                column: "normalized_role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_normalized_user_name",
                table: "users",
                column: "normalized_user_name",
                unique: true,
                filter: "[normalized_user_name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_user_claims_claim_id",
                table: "user_claims",
                column: "claim_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                table: "user_roles",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_tokens");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "claims");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
