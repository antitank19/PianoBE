using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Data.Migrations
{
    public partial class ChangeMusicFileToMidiFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SheetFile",
                table: "Sheets",
                newName: "MidiFile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MidiFile",
                table: "Sheets",
                newName: "SheetFile");
        }
    }
}
