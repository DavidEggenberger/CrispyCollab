using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class nasdfdgfsadfotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_CreatorId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Teams_TeamId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "AdminNotifications");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_TeamId",
                table: "AdminNotifications",
                newName: "IX_AdminNotifications_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_CreatorId",
                table: "AdminNotifications",
                newName: "IX_AdminNotifications_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminNotifications",
                table: "AdminNotifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminNotifications_AspNetUsers_CreatorId",
                table: "AdminNotifications",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminNotifications_Teams_TeamId",
                table: "AdminNotifications",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminNotifications_AspNetUsers_CreatorId",
                table: "AdminNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminNotifications_Teams_TeamId",
                table: "AdminNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminNotifications",
                table: "AdminNotifications");

            migrationBuilder.RenameTable(
                name: "AdminNotifications",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_AdminNotifications_TeamId",
                table: "Notifications",
                newName: "IX_Notifications_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_AdminNotifications_CreatorId",
                table: "Notifications",
                newName: "IX_Notifications_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_CreatorId",
                table: "Notifications",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Teams_TeamId",
                table: "Notifications",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
