using Microsoft.EntityFrameworkCore.Migrations;

namespace Fore_Golf.Data.Migrations
{
    public partial class addingmatchnameandlocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Matches",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Matches");
        }
    }
}
