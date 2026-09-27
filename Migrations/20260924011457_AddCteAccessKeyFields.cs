using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmbarcaPro.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCteAccessKeyFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IssuenceType",
                table: "ctes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "numeric_code",
                table: "ctes",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssuenceType",
                table: "ctes");

            migrationBuilder.DropColumn(
                name: "numeric_code",
                table: "ctes");
        }
    }
}
