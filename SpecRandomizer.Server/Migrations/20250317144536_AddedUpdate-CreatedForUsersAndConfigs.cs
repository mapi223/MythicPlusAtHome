using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpecRandomizer.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedUpdateCreatedForUsersAndConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Configurations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "Configurations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ModifiedByUserId",
                table: "Users",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_ModifiedByUserId",
                table: "Configurations",
                column: "ModifiedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_Users_ModifiedByUserId",
                table: "Configurations",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ModifiedByUserId",
                table: "Users",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_Users_ModifiedByUserId",
                table: "Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ModifiedByUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ModifiedByUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_ModifiedByUserId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Configurations");
        }
    }
}
