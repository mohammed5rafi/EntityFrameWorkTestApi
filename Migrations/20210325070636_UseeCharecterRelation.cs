using Microsoft.EntityFrameworkCore.Migrations;

namespace Webapi.Migrations
{
    public partial class UseeCharecterRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Charecters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charecters_UserId",
                table: "Charecters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Charecters_Users_UserId",
                table: "Charecters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charecters_Users_UserId",
                table: "Charecters");

            migrationBuilder.DropIndex(
                name: "IX_Charecters_UserId",
                table: "Charecters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Charecters");
        }
    }
}
