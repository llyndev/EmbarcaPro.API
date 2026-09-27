using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmbarcaPro.API.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureCteTakerColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Taker",
                table: "ctes",
                newName: "taker");

            migrationBuilder.AlterColumn<string>(
                name: "taker",
                table: "ctes",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "taker",
                table: "ctes",
                newName: "Taker");

            migrationBuilder.AlterColumn<int>(
                name: "Taker",
                table: "ctes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldMaxLength: 5);
        }
    }
}
