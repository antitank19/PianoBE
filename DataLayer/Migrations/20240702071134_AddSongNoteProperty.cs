using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class AddSongNoteProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Instruments_InstrumentID",
                table: "Sheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Songs_SongID",
                table: "Sheets");

            migrationBuilder.DropForeignKey(
                name: "FK_SongNote_Notes_NoteID",
                table: "SongNote");

            migrationBuilder.DropForeignKey(
                name: "FK_SongNote_Sheets_SheetID",
                table: "SongNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongNote",
                table: "SongNote");

            migrationBuilder.RenameTable(
                name: "SongNote",
                newName: "SongNotes");

            migrationBuilder.RenameColumn(
                name: "SongID",
                table: "Sheets",
                newName: "SongId");

            migrationBuilder.RenameColumn(
                name: "InstrumentID",
                table: "Sheets",
                newName: "InstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Sheets_SongID",
                table: "Sheets",
                newName: "IX_Sheets_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_Sheets_InstrumentID",
                table: "Sheets",
                newName: "IX_Sheets_InstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_SongNote_SheetID",
                table: "SongNotes",
                newName: "IX_SongNotes_SheetID");

            migrationBuilder.RenameIndex(
                name: "IX_SongNote_NoteID",
                table: "SongNotes",
                newName: "IX_SongNotes_NoteID");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "SongNotes",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Chromatic",
                table: "SongNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongNotes",
                table: "SongNotes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Instruments_InstrumentId",
                table: "Sheets",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Songs_SongId",
                table: "Sheets",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongNotes_Notes_NoteID",
                table: "SongNotes",
                column: "NoteID",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongNotes_Sheets_SheetID",
                table: "SongNotes",
                column: "SheetID",
                principalTable: "Sheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Instruments_InstrumentId",
                table: "Sheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Songs_SongId",
                table: "Sheets");

            migrationBuilder.DropForeignKey(
                name: "FK_SongNotes_Notes_NoteID",
                table: "SongNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_SongNotes_Sheets_SheetID",
                table: "SongNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongNotes",
                table: "SongNotes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Chromatic",
                table: "SongNotes");

            migrationBuilder.RenameTable(
                name: "SongNotes",
                newName: "SongNote");

            migrationBuilder.RenameColumn(
                name: "SongId",
                table: "Sheets",
                newName: "SongID");

            migrationBuilder.RenameColumn(
                name: "InstrumentId",
                table: "Sheets",
                newName: "InstrumentID");

            migrationBuilder.RenameIndex(
                name: "IX_Sheets_SongId",
                table: "Sheets",
                newName: "IX_Sheets_SongID");

            migrationBuilder.RenameIndex(
                name: "IX_Sheets_InstrumentId",
                table: "Sheets",
                newName: "IX_Sheets_InstrumentID");

            migrationBuilder.RenameIndex(
                name: "IX_SongNotes_SheetID",
                table: "SongNote",
                newName: "IX_SongNote_SheetID");

            migrationBuilder.RenameIndex(
                name: "IX_SongNotes_NoteID",
                table: "SongNote",
                newName: "IX_SongNote_NoteID");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "SongNote",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongNote",
                table: "SongNote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Instruments_InstrumentID",
                table: "Sheets",
                column: "InstrumentID",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Songs_SongID",
                table: "Sheets",
                column: "SongID",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongNote_Notes_NoteID",
                table: "SongNote",
                column: "NoteID",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongNote_Sheets_SheetID",
                table: "SongNote",
                column: "SheetID",
                principalTable: "Sheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
