using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Data.Migrations
{
    public partial class AddXmlAndBackgroundMusicFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundMusicFile",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XmlFile",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundMusicFile",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "XmlFile",
                table: "Sheets");
        }
    }
}
