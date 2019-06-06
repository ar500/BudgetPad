using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetPad.Server.Migrations
{
    public partial class initial : Migration
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
                name: "budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DatePaid = table.Column<DateTime>(nullable: false),
                    AmountSpent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BudgetCategoryId = table.Column<Guid>(nullable: false),
                    BudgetId = table.Column<Guid>(nullable: true),
                    EntryDateTime = table.Column<DateTime>(nullable: false),
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
                    table.PrimaryKey("PK_budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_budgets_Categories_BudgetCategoryId",
                        column: x => x.BudgetCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_budgets_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DatePaid = table.Column<DateTime>(nullable: false),
                    AmountSpent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BudgetCategoryId = table.Column<Guid>(nullable: false),
                    BudgetId = table.Column<Guid>(nullable: true),
                    EntryDateTime = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true),
                    ExpenseId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseEntries_Categories_BudgetCategoryId",
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
                    DatePaid = table.Column<DateTime>(nullable: false),
                    AmountSpent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BudgetCategoryId = table.Column<Guid>(nullable: false),
                    BudgetId = table.Column<Guid>(nullable: true),
                    EntryDateTime = table.Column<DateTime>(nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_budgets_BudgetCategoryId",
                table: "budgets",
                column: "BudgetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_budgets_BudgetId",
                table: "budgets",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseEntries_BudgetCategoryId",
                table: "ExpenseEntries",
                column: "BudgetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UnplannedExpenses_BudgetCategoryId",
                table: "UnplannedExpenses",
                column: "BudgetCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "budgets");

            migrationBuilder.DropTable(
                name: "ExpenseEntries");

            migrationBuilder.DropTable(
                name: "UnplannedExpenses");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
