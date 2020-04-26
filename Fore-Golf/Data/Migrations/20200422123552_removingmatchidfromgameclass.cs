using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fore_Golf.Data.Migrations
{
    public partial class removingmatchidfromgameclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Matches_Id",
                table: "Games");

            migrationBuilder.AlterColumn<Guid>(
                name: "MatchId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Games_MatchId",
                table: "Games",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Matches_MatchId",
                table: "Games",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Matches_MatchId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_MatchId",
                table: "Games");

            migrationBuilder.AlterColumn<Guid>(
                name: "MatchId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Matches_Id",
                table: "Games",
                column: "Id",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
