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
                name: "ExpenseBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DatePaid = table.Column<DateTime>(nullable: false),
                    AmountSpent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BudgetId = table.Column<Guid>(nullable: true),
                    EntryDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    BudgetCategoryId = table.Column<Guid>(nullable: true),
                    ShortName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    AmountPlanned = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: true),
                    PayoutAccountNumber = table.Column<string>(maxLength: 100, nullable: true),
                    ExpenseLogEntry_BudgetCategoryId = table.Column<Guid>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true),
                    UnplannedExpense_BudgetCategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseBase_Categories_BudgetCategoryId",
                        column: x => x.BudgetCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseBase_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseBase_Categories_ExpenseLogEntry_BudgetCategoryId",
                        column: x => x.ExpenseLogEntry_BudgetCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseBase_Categories_UnplannedExpense_BudgetCategoryId",
                        column: x => x.UnplannedExpense_BudgetCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBase_BudgetCategoryId",
                table: "ExpenseBase",
                column: "BudgetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBase_BudgetId",
                table: "ExpenseBase",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBase_ExpenseLogEntry_BudgetCategoryId",
                table: "ExpenseBase",
                column: "ExpenseLogEntry_BudgetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseBase_UnplannedExpense_BudgetCategoryId",
                table: "ExpenseBase",
                column: "UnplannedExpense_BudgetCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseBase");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Budgets");
        }
    }
}
