using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Identity.EFCore.Migrations
{
    public partial class asdfasdfsfd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Plan",
                table: "Teams");

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionPlanId",
                table: "Teams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StripeSubscriptionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_SubscriptionPlanId",
                table: "Teams",
                column: "SubscriptionPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_SubscriptionPlans_SubscriptionPlanId",
                table: "Teams",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_SubscriptionPlans_SubscriptionPlanId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_Teams_SubscriptionPlanId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "SubscriptionPlanId",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "Plan",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
