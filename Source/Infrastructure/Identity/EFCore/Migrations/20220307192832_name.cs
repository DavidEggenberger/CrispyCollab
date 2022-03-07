using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Identity.EFCore.Migrations
{
    public partial class name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconData",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "NameIdentitifer",
                table: "Teams",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teams",
                newName: "NameIdentitifer");

            migrationBuilder.AddColumn<byte[]>(
                name: "IconData",
                table: "Teams",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
