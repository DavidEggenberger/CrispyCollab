using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Identity.EFCore.Migrations
{
    public partial class assdafasdfdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_SubscriptionPlans_SubscriptionPlanId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_SubscriptionPlanId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "SubscriptionPlanId",
                table: "Teams",
                newName: "SubscriptionId");

            migrationBuilder.RenameColumn(
                name: "StripeSubscriptionId",
                table: "SubscriptionPlans",
                newName: "StripePriceId");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StripeSubscriptionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionPlanId",
                table: "Subscriptions",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_TeamId",
                table: "Subscriptions",
                column: "TeamId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Teams",
                newName: "SubscriptionPlanId");

            migrationBuilder.RenameColumn(
                name: "StripePriceId",
                table: "SubscriptionPlans",
                newName: "StripeSubscriptionId");

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
    }
}
