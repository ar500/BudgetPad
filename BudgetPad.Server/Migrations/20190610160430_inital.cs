using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetPad.Server.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ShortName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    CycleStartDate = table.Column<DateTime>(nullable: false),
                    CycleEndDate = table.Column<DateTime>(nullable: false),
                    AllottedFunds = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OwnerEmail = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ShortName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BudgetId = table.Column<Guid>(nullable: true),
                    BudgetCategoryId = table.Column<Guid>(nullable: false),
                    EntryDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShortName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    AmountPlanned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: true),
                    PayoutAccountNumber = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Categories_BudgetCategoryId",
                        column: x => x.BudgetCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BudgetId = table.Column<Guid>(nullable: true),
                    BudgetCategoryId = table.Column<Guid>(nullable: false),
                    EntryDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseLogs_Categories_BudgetCategoryId",
                        column: x => x.BudgetCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnplannedExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BudgetId = table.Column<Guid>(nullable: true),
                    BudgetCategoryId = table.Column<Guid>(nullable: false),
                    EntryDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnplannedExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnplannedExpenses_Categories_BudgetCategoryId",
                        column: x => x.BudgetCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatePaid = table.Column<DateTime>(nullable: false),
                    EntryDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    BillId = table.Column<Guid>(nullable: true),
                    ExpenseLogEntryId = table.Column<Guid>(nullable: true),
                    UnplannedExpenseId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_ExpenseLogs_ExpenseLogEntryId",
                        column: x => x.ExpenseLogEntryId,
                        principalTable: "ExpenseLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_UnplannedExpenses_UnplannedExpenseId",
                        column: x => x.UnplannedExpenseId,
                        principalTable: "UnplannedExpenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BudgetCategoryId",
                table: "Bills",
                column: "BudgetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BudgetId",
                table: "Bills",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseLogs_BudgetCategoryId",
                table: "ExpenseLogs",
                column: "BudgetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillId",
                table: "Payments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ExpenseLogEntryId",
                table: "Payments",
                column: "ExpenseLogEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UnplannedExpenseId",
                table: "Payments",
                column: "UnplannedExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_UnplannedExpenses_BudgetCategoryId",
                table: "UnplannedExpenses",
                column: "BudgetCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "ExpenseLogs");

            migrationBuilder.DropTable(
                name: "UnplannedExpenses");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
