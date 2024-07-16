using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class LeftHandSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeftHandSheetId",
                table: "Sheets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sheets_LeftHandSheetId",
                table: "Sheets",
                column: "LeftHandSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Sheets_LeftHandSheetId",
                table: "Sheets",
                column: "LeftHandSheetId",
                principalTable: "Sheets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Sheets_LeftHandSheetId",
                table: "Sheets");

            migrationBuilder.DropIndex(
                name: "IX_Sheets_LeftHandSheetId",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "LeftHandSheetId",
                table: "Sheets");
        }
    }
}
