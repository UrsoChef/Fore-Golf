using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fore_Golf.Data.Migrations
{
    public partial class addinggolfergamemappingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Golfers_Id",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Games_Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "MatchEndDate",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "MatchStartDate",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "StartingPosition",
                table: "Matches");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                table: "Golfers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                table: "Games",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GolferGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    GolferId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GolferGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GolferGames_Games_Id",
                        column: x => x.Id,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GolferGames_Golfers_Id",
                        column: x => x.Id,
                        principalTable: "Golfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Golfers_MatchId",
                table: "Golfers",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Matches_Id",
                table: "Games",
                column: "Id",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Golfers_Matches_MatchId",
                table: "Golfers",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Matches_Id",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Golfers_Matches_MatchId",
                table: "Golfers");

            migrationBuilder.DropTable(
                name: "GolferGames");

            migrationBuilder.DropIndex(
                name: "IX_Golfers_MatchId",
                table: "Golfers");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Golfers");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Games");

            migrationBuilder.AddColumn<DateTime>(
                name: "MatchEndDate",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MatchStartDate",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StartingPosition",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Golfers_Id",
                table: "Games",
                column: "Id",
                principalTable: "Golfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Games_Id",
                table: "Matches",
                column: "Id",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
