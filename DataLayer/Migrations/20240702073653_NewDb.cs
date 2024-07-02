using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class NewDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongNotes_Sheets_SheetID",
                table: "SongNotes");

            migrationBuilder.DropColumn(
                name: "Measure",
                table: "SongNotes");

            migrationBuilder.RenameColumn(
                name: "SheetID",
                table: "SongNotes",
                newName: "MeasureId");

            migrationBuilder.RenameIndex(
                name: "IX_SongNotes_SheetID",
                table: "SongNotes",
                newName: "IX_SongNotes_MeasureId");

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

            migrationBuilder.CreateTable(
                name: "Measure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SheetId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measure_Sheets_SheetId",
                        column: x => x.SheetId,
                        principalTable: "Sheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measure_SheetId",
                table: "Measure",
                column: "SheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongNotes_Measure_MeasureId",
                table: "SongNotes",
                column: "MeasureId",
                principalTable: "Measure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongNotes_Measure_MeasureId",
                table: "SongNotes");

            migrationBuilder.DropTable(
                name: "Measure");

            migrationBuilder.DropColumn(
                name: "Signature1",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Signature2",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "MeasureId",
                table: "SongNotes",
                newName: "SheetID");

            migrationBuilder.RenameIndex(
                name: "IX_SongNotes_MeasureId",
                table: "SongNotes",
                newName: "IX_SongNotes_SheetID");

            migrationBuilder.AddColumn<int>(
                name: "Measure",
                table: "SongNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SongNotes_Sheets_SheetID",
                table: "SongNotes",
                column: "SheetID",
                principalTable: "Sheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
