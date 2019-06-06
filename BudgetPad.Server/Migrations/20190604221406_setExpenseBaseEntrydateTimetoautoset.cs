using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetPad.Server.Migrations
{
    public partial class setExpenseBaseEntrydateTimetoautoset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnplannedExpenses_Categories_BudgetCategoryId",
                table: "UnplannedExpenses");

            migrationBuilder.DropTable(
                name: "budgets");

            migrationBuilder.DropTable(
                name: "ExpenseEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnplannedExpenses",
                table: "UnplannedExpenses");

            migrationBuilder.RenameTable(
                name: "UnplannedExpenses",
                newName: "ExpenseBase");

            migrationBuilder.RenameIndex(
                name: "IX_UnplannedExpenses_BudgetCategoryId",
                table: "ExpenseBase",
                newName: "IX_ExpenseBase_BudgetCategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EntryDateTime",
                table: "ExpenseBase",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<Guid>(
                name: "BudgetCategoryId",
                table: "ExpenseBase",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPlanned",
                table: "ExpenseBase",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "ExpenseBase",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ExpenseBase",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "ExpenseBase",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayoutAccountNumber",
                table: "ExpenseBase",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "ExpenseBase",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ExpenseBase",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseLogEntry_BudgetCategoryId",
                table: "ExpenseBase",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "ExpenseBase",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnplannedExpense_BudgetCategoryId",
                table: "ExpenseBase",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseBase",
                table: "ExpenseBase",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseBase_Categories_BudgetCategoryId",
                table: "ExpenseBase",
                column: "BudgetCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseBase_Budgets_BudgetId",
                table: "ExpenseBase",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseBase_Categories_ExpenseLogEntry_BudgetCategoryId",
                table: "ExpenseBase",
                column: "ExpenseLogEntry_BudgetCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseBase_Categories_UnplannedExpense_BudgetCategoryId",
                table: "ExpenseBase",
                column: "UnplannedExpense_BudgetCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseBase_Categories_BudgetCategoryId",
                table: "ExpenseBase");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseBase_Budgets_BudgetId",
                table: "ExpenseBase");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseBase_Categories_ExpenseLogEntry_BudgetCategoryId",
                table: "ExpenseBase");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseBase_Categories_UnplannedExpense_BudgetCategoryId",
                table: "ExpenseBase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseBase",
                table: "ExpenseBase");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseBase_BudgetId",
                table: "ExpenseBase");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseBase_ExpenseLogEntry_BudgetCategoryId",
                table: "ExpenseBase");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseBase_UnplannedExpense_BudgetCategoryId",
                table: "ExpenseBase");

            migrationBuilder.DropColumn(
                name: "AmountPlanned",
                table: "ExpenseBase");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "ExpenseBase");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ExpenseBase");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "ExpenseBase");

            migrationBuilder.DropColumn(
                name: "PayoutAccountNumber",
                table: "ExpenseBase");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "ExpenseBase");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ExpenseBase");

            migrationBuilder.DropColumn(
                name: "ExpenseLogEntry_BudgetCategoryId",
                table: "ExpenseBase");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "ExpenseBase");

            migrationBuilder.DropColumn(
                name: "UnplannedExpense_BudgetCategoryId",
                table: "ExpenseBase");

            migrationBuilder.RenameTable(
                name: "ExpenseBase",
                newName: "UnplannedExpenses");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseBase_BudgetCategoryId",
                table: "UnplannedExpenses",
                newName: "IX_UnplannedExpenses_BudgetCategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EntryDateTime",
                table: "UnplannedExpenses",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<Guid>(
                name: "BudgetCategoryId",
                table: "UnplannedExpenses",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnplannedExpenses",
                table: "UnplannedExpenses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AmountPlanned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountSpent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BudgetCategoryId = table.Column<Guid>(nullable: false),
                    BudgetId = table.Column<Guid>(nullable: true),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: true),
                    DatePaid = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    EntryDateTime = table.Column<DateTime>(nullable: false),
                    PayoutAccountNumber = table.Column<string>(maxLength: 100, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShortName = table.Column<string>(maxLength: 50, nullable: false)
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
                    AmountSpent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BudgetCategoryId = table.Column<Guid>(nullable: false),
                    BudgetId = table.Column<Guid>(nullable: true),
                    DatePaid = table.Column<DateTime>(nullable: false),
                    EntryDateTime = table.Column<DateTime>(nullable: false),
                    ExpenseId = table.Column<Guid>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_UnplannedExpenses_Categories_BudgetCategoryId",
                table: "UnplannedExpenses",
                column: "BudgetCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
