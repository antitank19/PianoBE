using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class MoveChromaticToChordNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature1",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Signature2",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Chromatic",
                table: "Chords");

            migrationBuilder.AddColumn<int>(
                name: "Chromatic",
                table: "ChordNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chromatic",
                table: "ChordNotes");

            migrationBuilder.AddColumn<int>(
                name: "Signature1",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Signature2",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Chromatic",
                table: "Chords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
