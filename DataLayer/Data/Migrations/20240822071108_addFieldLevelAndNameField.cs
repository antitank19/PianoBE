using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Data.Migrations
{
    public partial class addFieldLevelAndNameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Sheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sheets");
        }
    }
}
