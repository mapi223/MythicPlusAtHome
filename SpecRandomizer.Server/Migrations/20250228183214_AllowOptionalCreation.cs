using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpecRandomizer.Server.Migrations
{
    /// <inheritdoc />
    public partial class AllowOptionalCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Configurations_ConfigurationId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationId",
                table: "Players",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ConfiguratioId",
                table: "Players",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Configurations_ConfigurationId",
                table: "Players",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "ConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Configurations_ConfigurationId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationId",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConfiguratioId",
                table: "Players",
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
                principalColumn: "ConfigurationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
