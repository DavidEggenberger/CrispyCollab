using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class foasdfurt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTenant_AspNetUsers_UserId",
                table: "ApplicationUserTenant");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTenant_Tenants_TenantId",
                table: "ApplicationUserTenant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserTenant",
                table: "ApplicationUserTenant");

            migrationBuilder.RenameTable(
                name: "ApplicationUserTenant",
                newName: "ApplicationUserTenants");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserTenant_UserId",
                table: "ApplicationUserTenants",
                newName: "IX_ApplicationUserTenants_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserTenant_TenantId",
                table: "ApplicationUserTenants",
                newName: "IX_ApplicationUserTenants_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserTenants",
                table: "ApplicationUserTenants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTenants_AspNetUsers_UserId",
                table: "ApplicationUserTenants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTenants_Tenants_TenantId",
                table: "ApplicationUserTenants",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTenants_AspNetUsers_UserId",
                table: "ApplicationUserTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTenants_Tenants_TenantId",
                table: "ApplicationUserTenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserTenants",
                table: "ApplicationUserTenants");

            migrationBuilder.RenameTable(
                name: "ApplicationUserTenants",
                newName: "ApplicationUserTenant");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserTenants_UserId",
                table: "ApplicationUserTenant",
                newName: "IX_ApplicationUserTenant_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserTenants_TenantId",
                table: "ApplicationUserTenant",
                newName: "IX_ApplicationUserTenant_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserTenant",
                table: "ApplicationUserTenant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTenant_AspNetUsers_UserId",
                table: "ApplicationUserTenant",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTenant_Tenants_TenantId",
                table: "ApplicationUserTenant",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
