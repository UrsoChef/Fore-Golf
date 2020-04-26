using Microsoft.EntityFrameworkCore.Migrations;

namespace Fore_Golf.Data.Migrations
{
    public partial class movedscoretogolfergame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "GolferGames",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "GolferGames");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
