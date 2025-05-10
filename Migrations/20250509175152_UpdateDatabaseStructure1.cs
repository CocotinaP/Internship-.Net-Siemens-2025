using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.NetSiemens2025.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseStructure1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailablilityNumber",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailablilityNumber",
                table: "Books");
        }
    }
}
