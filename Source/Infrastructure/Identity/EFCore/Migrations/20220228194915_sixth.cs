using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Identity.EFCore.Migrations
{
    public partial class sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamInvitedUser");

            migrationBuilder.DropTable(
                name: "InvitedUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserTeams",
                table: "ApplicationUserTeams");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserTeams_UserId",
                table: "ApplicationUserTeams");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ApplicationUserTeams");

            migrationBuilder.AddColumn<string>(
                name: "StripeCustomerId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserTeams",
                table: "ApplicationUserTeams",
                columns: new[] { "UserId", "TeamId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserTeams",
                table: "ApplicationUserTeams");

            migrationBuilder.DropColumn(
                name: "StripeCustomerId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ApplicationUserTeams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserTeams",
                table: "ApplicationUserTeams",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InvitedUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitedUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamInvitedUser",
                columns: table => new
                {
                    InvitedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamInvitedUser", x => new { x.InvitedUserId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamInvitedUser_InvitedUser_InvitedUserId",
                        column: x => x.InvitedUserId,
                        principalTable: "InvitedUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamInvitedUser_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTeams_UserId",
                table: "ApplicationUserTeams",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamInvitedUser_TeamId",
                table: "TeamInvitedUser",
                column: "TeamId");
        }
    }
}
