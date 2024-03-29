﻿// <auto-generated />
using System;
using BudgetPad.Server.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BudgetPad.Server.Migrations
{
    [DbContext(typeof(BudgetPadContext))]
    [Migration("20190615222818_addedUnplannedExpenses_to_Budget")]
    partial class addedUnplannedExpenses_to_Budget
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview5.19227.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BudgetPad.Shared.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AmountPlanned")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("BudgetCategoryId");

                    b.Property<Guid?>("BudgetId");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("DueDate");

                    b.Property<DateTime>("EntryDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("PayoutAccountNumber")
                        .HasMaxLength(100);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("BudgetCategoryId");

                    b.HasIndex("BudgetId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("BudgetPad.Shared.Budget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AllottedFunds")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CycleEndDate");

                    b.Property<DateTime>("CycleStartDate");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("OwnerEmail")
                        .IsRequired();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("BudgetPad.Shared.BudgetCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BudgetPad.Shared.ExpenseLogEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EntryDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("ExpenseId");

                    b.Property<Guid>("PaymentId");

                    b.Property<string>("Remarks")
                        .HasMaxLength(200);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.ToTable("ExpenseLogs");
                });

            modelBuilder.Entity("BudgetPad.Shared.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("BillId");

                    b.Property<DateTime>("DatePaid");

                    b.Property<DateTime>("EntryDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid?>("UnplannedExpenseId");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("UnplannedExpenseId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("BudgetPad.Shared.UnplannedExpense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BudgetCategoryId");

                    b.Property<Guid?>("BudgetId");

                    b.Property<DateTime>("EntryDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("BudgetCategoryId");

                    b.HasIndex("BudgetId");

                    b.ToTable("UnplannedExpenses");
                });

            modelBuilder.Entity("BudgetPad.Shared.Bill", b =>
                {
                    b.HasOne("BudgetPad.Shared.BudgetCategory", "BudgetCategory")
                        .WithMany()
                        .HasForeignKey("BudgetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetPad.Shared.Budget", null)
                        .WithMany("Bills")
                        .HasForeignKey("BudgetId");
                });

            modelBuilder.Entity("BudgetPad.Shared.ExpenseLogEntry", b =>
                {
                    b.HasOne("BudgetPad.Shared.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BudgetPad.Shared.Payment", b =>
                {
                    b.HasOne("BudgetPad.Shared.Bill", null)
                        .WithMany("Payments")
                        .HasForeignKey("BillId");

                    b.HasOne("BudgetPad.Shared.UnplannedExpense", null)
                        .WithMany("Payments")
                        .HasForeignKey("UnplannedExpenseId");
                });

            modelBuilder.Entity("BudgetPad.Shared.UnplannedExpense", b =>
                {
                    b.HasOne("BudgetPad.Shared.BudgetCategory", "BudgetCategory")
                        .WithMany()
                        .HasForeignKey("BudgetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetPad.Shared.Budget", null)
                        .WithMany("UnplannedExpenses")
                        .HasForeignKey("BudgetId");
                });
#pragma warning restore 612, 618
        }
    }
}
