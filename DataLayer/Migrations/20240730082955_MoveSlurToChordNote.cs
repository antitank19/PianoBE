using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class MoveSlurToChordNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlurPosition",
                table: "Chords");

            migrationBuilder.AddColumn<int>(
                name: "SlurPosition",
                table: "ChordNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlurPosition",
                table: "ChordNotes");

            migrationBuilder.AddColumn<int>(
                name: "SlurPosition",
                table: "Chords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
