using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmbarcaPro.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCteTaker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IssuenceType",
                table: "ctes",
                newName: "issuence_type");

            migrationBuilder.AddColumn<int>(
                name: "Taker",
                table: "ctes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taker",
                table: "ctes");

            migrationBuilder.RenameColumn(
                name: "issuence_type",
                table: "ctes",
                newName: "IssuenceType");
        }
    }
}
