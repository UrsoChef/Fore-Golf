using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fore_Golf.Data.Migrations
{
    public partial class renamegolfergametogamegolfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GolferGames_Games_Id",
                table: "GolferGames");

            migrationBuilder.DropForeignKey(
                name: "FK_GolferGames_Golfers_Id",
                table: "GolferGames");

            migrationBuilder.AlterColumn<Guid>(
                name: "GolferId",
                table: "GolferGames",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "GameId",
                table: "GolferGames",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_GolferGames_GameId",
                table: "GolferGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GolferGames_GolferId",
                table: "GolferGames",
                column: "GolferId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GolferGames_Games_GameId",
                table: "GolferGames");

            migrationBuilder.DropForeignKey(
                name: "FK_GolferGames_Golfers_GolferId",
                table: "GolferGames");

            migrationBuilder.DropIndex(
                name: "IX_GolferGames_GameId",
                table: "GolferGames");

            migrationBuilder.DropIndex(
                name: "IX_GolferGames_GolferId",
                table: "GolferGames");

            migrationBuilder.AlterColumn<Guid>(
                name: "GolferId",
                table: "GolferGames",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GameId",
                table: "GolferGames",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GolferGames_Games_Id",
                table: "GolferGames",
                column: "Id",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GolferGames_Golfers_Id",
                table: "GolferGames",
                column: "Id",
                principalTable: "Golfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
