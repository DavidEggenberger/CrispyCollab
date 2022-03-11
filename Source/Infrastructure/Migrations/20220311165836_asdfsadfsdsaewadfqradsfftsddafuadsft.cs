using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class asdfsadfsdsaewadfqradsfftsddafuadsft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_DefaultTeamId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DefaultTeamId",
                table: "AspNetUsers",
                newName: "SelectedTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DefaultTeamId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_SelectedTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_SelectedTeamId",
                table: "AspNetUsers",
                column: "SelectedTeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_SelectedTeamId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SelectedTeamId",
                table: "AspNetUsers",
                newName: "DefaultTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_SelectedTeamId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DefaultTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_DefaultTeamId",
                table: "AspNetUsers",
                column: "DefaultTeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
