using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fore_Golf.Data.Migrations
{
    public partial class AddingForeignKeysInDatabaseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGolfers_Games_GameId",
                table: "GameGolfers");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGolfers_Golfers_GolferId",
                table: "GameGolfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Matches_MatchId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Golfers_Matches_MatchId",
                table: "Golfers");

            migrationBuilder.DropIndex(
                name: "IX_Golfers_MatchId",
                table: "Golfers");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Golfers");

            migrationBuilder.AlterColumn<Guid>(
                name: "MatchId",
                table: "Games",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GolferId",
                table: "GameGolfers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GameId",
                table: "GameGolfers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGolfers_Games_GameId",
                table: "GameGolfers",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGolfers_Golfers_GolferId",
                table: "GameGolfers",
                column: "GolferId",
                principalTable: "Golfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Matches_MatchId",
                table: "Games",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGolfers_Games_GameId",
                table: "GameGolfers");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGolfers_Golfers_GolferId",
                table: "GameGolfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Matches_MatchId",
                table: "Games");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                table: "Golfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MatchId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "GolferId",
                table: "GameGolfers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "GameId",
                table: "GameGolfers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Golfers_MatchId",
                table: "Golfers",
                column: "MatchId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Matches_MatchId",
                table: "Games",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Golfers_Matches_MatchId",
                table: "Golfers",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
