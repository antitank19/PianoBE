using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Data.Migrations
{
    public partial class AddPlayerToPlayTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "PlayTracking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlayTracking_PlayerId",
                table: "PlayTracking",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayTracking_AspNetUsers_PlayerId",
                table: "PlayTracking",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayTracking_AspNetUsers_PlayerId",
                table: "PlayTracking");

            migrationBuilder.DropIndex(
                name: "IX_PlayTracking_PlayerId",
                table: "PlayTracking");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayTracking");
        }
    }
}
