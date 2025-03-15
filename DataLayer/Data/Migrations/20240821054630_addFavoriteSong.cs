using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Data.Migrations
{
    public partial class addFavoriteSong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_AspNetUsers_ArtistId",
                table: "Songs");

            migrationBuilder.CreateTable(
                name: "SongUser",
                columns: table => new
                {
                    FavoriteSongId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongUser", x => new { x.FavoriteSongId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_SongUser_AspNetUsers_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongUser_Songs_FavoriteSongId",
                        column: x => x.FavoriteSongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongUser_PlayersId",
                table: "SongUser",
                column: "PlayersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_AspNetUsers_ArtistId",
                table: "Songs",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_AspNetUsers_ArtistId",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "SongUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_AspNetUsers_ArtistId",
                table: "Songs",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
