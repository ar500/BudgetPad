using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetPad.Server.Migrations
{
    public partial class addedUnplannedExpenses_to_Budget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UnplannedExpenses_BudgetId",
                table: "UnplannedExpenses",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnplannedExpenses_Budgets_BudgetId",
                table: "UnplannedExpenses",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnplannedExpenses_Budgets_BudgetId",
                table: "UnplannedExpenses");

            migrationBuilder.DropIndex(
                name: "IX_UnplannedExpenses_BudgetId",
                table: "UnplannedExpenses");
        }
    }
}
