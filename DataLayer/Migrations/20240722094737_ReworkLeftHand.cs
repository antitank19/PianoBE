using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class ReworkLeftHand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measures_Sheets_SheetId",
                table: "Measures");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Sheets_LeftHandSheetId",
                table: "Sheets");

            migrationBuilder.DropIndex(
                name: "IX_Sheets_LeftHandSheetId",
                table: "Sheets");

            migrationBuilder.DropIndex(
                name: "IX_Measures_SheetId",
                table: "Measures");

            migrationBuilder.DropColumn(
                name: "LeftHandSheetId",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "SheetId",
                table: "Measures");

            migrationBuilder.AddColumn<int>(
                name: "LeftSheetId",
                table: "Measures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightSheetId",
                table: "Measures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measures_LeftSheetId",
                table: "Measures",
                column: "LeftSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Measures_RightSheetId",
                table: "Measures",
                column: "RightSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measures_Sheets_LeftSheetId",
                table: "Measures",
                column: "LeftSheetId",
                principalTable: "Sheets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measures_Sheets_RightSheetId",
                table: "Measures",
                column: "RightSheetId",
                principalTable: "Sheets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measures_Sheets_LeftSheetId",
                table: "Measures");

            migrationBuilder.DropForeignKey(
                name: "FK_Measures_Sheets_RightSheetId",
                table: "Measures");

            migrationBuilder.DropIndex(
                name: "IX_Measures_LeftSheetId",
                table: "Measures");

            migrationBuilder.DropIndex(
                name: "IX_Measures_RightSheetId",
                table: "Measures");

            migrationBuilder.DropColumn(
                name: "LeftSheetId",
                table: "Measures");

            migrationBuilder.DropColumn(
                name: "RightSheetId",
                table: "Measures");

            migrationBuilder.AddColumn<int>(
                name: "LeftHandSheetId",
                table: "Sheets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SheetId",
                table: "Measures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sheets_LeftHandSheetId",
                table: "Sheets",
                column: "LeftHandSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Measures_SheetId",
                table: "Measures",
                column: "SheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measures_Sheets_SheetId",
                table: "Measures",
                column: "SheetId",
                principalTable: "Sheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Sheets_LeftHandSheetId",
                table: "Sheets",
                column: "LeftHandSheetId",
                principalTable: "Sheets",
                principalColumn: "Id");
        }
    }
}
