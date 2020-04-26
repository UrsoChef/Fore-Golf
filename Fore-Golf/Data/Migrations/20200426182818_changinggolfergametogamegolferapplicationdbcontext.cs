using Microsoft.EntityFrameworkCore.Migrations;

namespace Fore_Golf.Data.Migrations
{
    public partial class changinggolfergametogamegolferapplicationdbcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GolferGames_Games_GameId",
                table: "GolferGames");

            migrationBuilder.DropForeignKey(
                name: "FK_GolferGames_Golfers_GolferId",
                table: "GolferGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GolferGames",
                table: "GolferGames");

            migrationBuilder.RenameTable(
                name: "GolferGames",
                newName: "GameGolfers");

            migrationBuilder.RenameIndex(
                name: "IX_GolferGames_GolferId",
                table: "GameGolfers",
                newName: "IX_GameGolfers_GolferId");

            migrationBuilder.RenameIndex(
                name: "IX_GolferGames_GameId",
                table: "GameGolfers",
                newName: "IX_GameGolfers_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameGolfers",
                table: "GameGolfers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGolfers_Games_GameId",
                table: "GameGolfers",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGolfers_Golfers_GolferId",
                table: "GameGolfers",
                column: "GolferId",
                principalTable: "Golfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGolfers_Games_GameId",
                table: "GameGolfers");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGolfers_Golfers_GolferId",
                table: "GameGolfers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameGolfers",
                table: "GameGolfers");

            migrationBuilder.RenameTable(
                name: "GameGolfers",
                newName: "GolferGames");

            migrationBuilder.RenameIndex(
                name: "IX_GameGolfers_GolferId",
                table: "GolferGames",
                newName: "IX_GolferGames_GolferId");

            migrationBuilder.RenameIndex(
                name: "IX_GameGolfers_GameId",
                table: "GolferGames",
                newName: "IX_GolferGames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GolferGames",
                table: "GolferGames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GolferGames_Games_GameId",
                table: "GolferGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GolferGames_Golfers_GolferId",
                table: "GolferGames",
                column: "GolferId",
                principalTable: "Golfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
