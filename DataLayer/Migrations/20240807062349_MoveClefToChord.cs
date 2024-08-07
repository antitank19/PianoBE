using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class MoveClefToChord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clef",
                table: "Measures");

            migrationBuilder.AddColumn<int>(
                name: "Clef",
                table: "Chords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clef",
                table: "Chords");

            migrationBuilder.AddColumn<int>(
                name: "Clef",
                table: "Measures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
