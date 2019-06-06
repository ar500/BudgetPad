﻿// <auto-generated />
using System;
using BudgetPad.Server.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BudgetPad.Server.Migrations
{
    [DbContext(typeof(BudgetPadContext))]
    partial class BudgetPadContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview5.19227.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("BudgetPad.Shared.ExpenseBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AmountSpent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("BudgetId");

                    b.Property<DateTime>("DatePaid");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("EntryDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("ExpenseBase");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ExpenseBase");
                });

            modelBuilder.Entity("BudgetPad.Shared.Bill", b =>
                {
                    b.HasBaseType("BudgetPad.Shared.ExpenseBase");

                    b.Property<decimal>("AmountPlanned")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("BudgetCategoryId");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("DueDate");

                    b.Property<string>("PayoutAccountNumber")
                        .HasMaxLength(100);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasIndex("BudgetCategoryId");

                    b.HasIndex("BudgetId");

                    b.HasDiscriminator().HasValue("Bill");
                });

            modelBuilder.Entity("BudgetPad.Shared.ExpenseLogEntry", b =>
                {
                    b.HasBaseType("BudgetPad.Shared.ExpenseBase");

                    b.Property<Guid>("BudgetCategoryId")
                        .HasColumnName("ExpenseLogEntry_BudgetCategoryId");

                    b.Property<string>("Remarks")
                        .HasMaxLength(200);

                    b.HasIndex("BudgetCategoryId");

                    b.HasDiscriminator().HasValue("ExpenseLogEntry");
                });

            modelBuilder.Entity("BudgetPad.Shared.UnplannedExpense", b =>
                {
                    b.HasBaseType("BudgetPad.Shared.ExpenseBase");

                    b.Property<Guid>("BudgetCategoryId")
                        .HasColumnName("UnplannedExpense_BudgetCategoryId");

                    b.HasIndex("BudgetCategoryId");

                    b.HasDiscriminator().HasValue("UnplannedExpense");
                });

            modelBuilder.Entity("BudgetPad.Shared.Bill", b =>
                {
                    b.HasOne("BudgetPad.Shared.BudgetCategory", "BudgetCategory")
                        .WithMany("budgets")
                        .HasForeignKey("BudgetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetPad.Shared.Budget", null)
                        .WithMany("budgets")
                        .HasForeignKey("BudgetId");
                });

            modelBuilder.Entity("BudgetPad.Shared.ExpenseLogEntry", b =>
                {
                    b.HasOne("BudgetPad.Shared.BudgetCategory", "BudgetCategory")
                        .WithMany()
                        .HasForeignKey("BudgetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BudgetPad.Shared.UnplannedExpense", b =>
                {
                    b.HasOne("BudgetPad.Shared.BudgetCategory", "BudgetCategory")
                        .WithMany()
                        .HasForeignKey("BudgetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
