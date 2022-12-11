using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthenticationService.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claim",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "varchar(255)", nullable: false),
                    Issuer = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "varchar(255)", nullable: false),
                    NormalizeRole = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "varchar(255)", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "varchar(255)", nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    IpAddresses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    token = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Expired = table.Column<DateTime>(type: "datetime", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Jti = table.Column<string>(type: "varchar(255)", nullable: true),
                    IpAddress = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "varchar(255)", nullable: true),
                    Expired = table.Column<DateTime>(type: "datetime", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    Jti = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => new { x.UserId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_UserClaim_Claim_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Claim",
                columns: new[] { "Id", "Issuer", "Type", "Value" },
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
                table: "Role",
                columns: new[] { "Id", "Description", "NormalizeRole", "Role" },
                values: new object[,]
                {
                    { new Guid("a404953c-d47b-4075-9027-0a4350f52160"), "Admin user", "ADMIN", "Admin" },
                    { new Guid("b582e5d0-6f55-4b2e-82a4-d9c4c58cb54a"), "Base user", "USER", "User" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Created", "IpAddresses", "IsActive", "IsDeleted", "NormalizedUserName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("e1a056cb-4854-43bd-baba-28fb6f3a8b4d"), new DateTime(2022, 12, 11, 21, 36, 30, 105, DateTimeKind.Local).AddTicks(9278), "0.0.0.1", true, false, "SUPERUSER", "xsZQBit3Q2p+6szmN3kadwhNrbYgyLjI|100|0d//leOyX3Rab9ZEoez4Xv91FRyTlzYa", "SuperUser" },
                    { new Guid("9ffc9b38-1ead-4373-a913-718f67165c3b"), new DateTime(2022, 12, 11, 21, 36, 30, 106, DateTimeKind.Local).AddTicks(9039), "0.0.0.1", true, false, "USER1", "mcaEigKpY/fMUDJ+guhfwihvkZFP8/1a|100|rVFQMn3mjsYonSzleF9Om9r3SFY/a+3J", "User1" }
                });

            migrationBuilder.InsertData(
                table: "UserClaim",
                columns: new[] { "ClaimId", "UserId" },
                values: new object[] { new Guid("9dd0342f-d827-4d82-915d-cef218c58c6f"), new Guid("9ffc9b38-1ead-4373-a913-718f67165c3b") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a404953c-d47b-4075-9027-0a4350f52160"), new Guid("e1a056cb-4854-43bd-baba-28fb6f3a8b4d") });

            migrationBuilder.CreateIndex(
                name: "IX_AccessToken_UserId",
                table: "AccessToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Jti",
                table: "RefreshToken",
                column: "Jti",
                unique: true,
                filter: "[Jti] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_NormalizeRole",
                table: "Role",
                column: "NormalizeRole",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_NormalizedUserName",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_ClaimId",
                table: "UserClaim",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessToken");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Claim");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
