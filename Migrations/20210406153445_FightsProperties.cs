using Microsoft.EntityFrameworkCore.Migrations;

namespace Webapi.Migrations
{
    public partial class FightsProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Defeats",
                table: "Charecters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fights",
                table: "Charecters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Victories",
                table: "Charecters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defeats",
                table: "Charecters");

            migrationBuilder.DropColumn(
                name: "Fights",
                table: "Charecters");

            migrationBuilder.DropColumn(
                name: "Victories",
                table: "Charecters");
        }
    }
}
