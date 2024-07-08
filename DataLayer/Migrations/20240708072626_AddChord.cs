using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class AddChord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongNotes");

            migrationBuilder.AddColumn<int>(
                name: "BottomSignature",
                table: "Sheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopSignature",
                table: "Sheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Chords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<float>(type: "real", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Chromatic = table.Column<int>(type: "int", nullable: false),
                    MeasureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chords_Measures_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChordNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteId = table.Column<int>(type: "int", nullable: false),
                    ChordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChordNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChordNotes_Chords_ChordId",
                        column: x => x.ChordId,
                        principalTable: "Chords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChordNotes_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChordNotes_ChordId",
                table: "ChordNotes",
                column: "ChordId");

            migrationBuilder.CreateIndex(
                name: "IX_ChordNotes_NoteId",
                table: "ChordNotes",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Chords_MeasureId",
                table: "Chords",
                column: "MeasureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChordNotes");

            migrationBuilder.DropTable(
                name: "Chords");

            migrationBuilder.DropColumn(
                name: "BottomSignature",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "TopSignature",
                table: "Sheets");

            migrationBuilder.CreateTable(
                name: "SongNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasureId = table.Column<int>(type: "int", nullable: false),
                    NoteID = table.Column<int>(type: "int", nullable: false),
                    Chromatic = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<float>(type: "real", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongNotes_Measures_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongNotes_Notes_NoteID",
                        column: x => x.NoteID,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongNotes_MeasureId",
                table: "SongNotes",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_SongNotes_NoteID",
                table: "SongNotes",
                column: "NoteID");
        }
    }
}
