using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpecRandomizer.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationConditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Configurations_ConfigurationId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Configurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Configurations_ConfigurationId",
                table: "Players",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "ConfigurationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Configurations_ConfigurationId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Configurations_ConfigurationId",
                table: "Players",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "ConfigurationId");
        }
    }
}
