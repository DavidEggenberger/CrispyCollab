using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class fourt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUri",
                table: "Tenants");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tenants",
                newName: "NameIdentitifer");

            migrationBuilder.AddColumn<byte[]>(
                name: "IconData",
                table: "Tenants",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconData",
                table: "Tenants");

            migrationBuilder.RenameColumn(
                name: "NameIdentitifer",
                table: "Tenants",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "IconUri",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
