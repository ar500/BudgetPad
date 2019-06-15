using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetPad.Server.Migrations
{
    public partial class removed_payments_from_expense_base_added_payment_UnplannedExpenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_UnplannedExpenses_UnplannedExpenseId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UnplannedExpenseId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UnplannedExpenseId",
                table: "Payments");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "UnplannedExpenses",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "UnplannedExpenses",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UnplannedExpenses_PaymentId",
                table: "UnplannedExpenses",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnplannedExpenses_Payments_PaymentId",
                table: "UnplannedExpenses",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnplannedExpenses_Payments_PaymentId",
                table: "UnplannedExpenses");

            migrationBuilder.DropIndex(
                name: "IX_UnplannedExpenses_PaymentId",
                table: "UnplannedExpenses");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "UnplannedExpenses");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "UnplannedExpenses");

            migrationBuilder.AddColumn<Guid>(
                name: "UnplannedExpenseId",
                table: "Payments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UnplannedExpenseId",
                table: "Payments",
                column: "UnplannedExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_UnplannedExpenses_UnplannedExpenseId",
                table: "Payments",
                column: "UnplannedExpenseId",
                principalTable: "UnplannedExpenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
