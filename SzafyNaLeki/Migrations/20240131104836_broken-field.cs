using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SzafyNaLeki.Migrations
{
    /// <inheritdoc />
    public partial class brokenfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyZepsuta",
                table: "Szafy",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyZepsuta",
                table: "Szafy");
        }
    }
}
