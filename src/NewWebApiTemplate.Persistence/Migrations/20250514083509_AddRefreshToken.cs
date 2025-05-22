using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewWebApiTemplate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Roles_RoleEntityId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UserEntityId",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "UserEntityId",
                table: "Roles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_UserEntityId",
                table: "Roles",
                newName: "IX_Roles_UserId");

            migrationBuilder.RenameColumn(
                name: "RoleEntityId",
                table: "Permissions",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_RoleEntityId",
                table: "Permissions",
                newName: "IX_Permissions_RoleId");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Roles_RoleId",
                table: "Permissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UserId",
                table: "Roles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Roles_RoleId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UserId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Roles",
                newName: "UserEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                newName: "IX_Roles_UserEntityId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Permissions",
                newName: "RoleEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                newName: "IX_Permissions_RoleEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Roles_RoleEntityId",
                table: "Permissions",
                column: "RoleEntityId",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UserEntityId",
                table: "Roles",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
