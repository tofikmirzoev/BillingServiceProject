﻿// <auto-generated />
using System;
using BillingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BillingAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240401131422_TestMigration")]
    partial class TestMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BillingAPI.Models.Account", b =>
                {
                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("AccountBalance")
                        .HasColumnType("float");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Removed")
                        .HasColumnType("bit");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BillingAPI.Models.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CustomerType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfBirth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BillingAPI.Models.CustomerAccount", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CustomerId", "AccountId");

                    b.HasIndex("AccountId");

                    b.ToTable("CustomerAccounts");
                });

            modelBuilder.Entity("BillingAPI.Models.Deposits", b =>
                {
                    b.Property<string>("DepositId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CloseDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("DepositBalance")
                        .HasColumnType("float");

                    b.Property<string>("DepositStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DepositTerm")
                        .HasColumnType("bigint");

                    b.Property<double>("InitialDepositBalance")
                        .HasColumnType("float");

                    b.Property<float>("InterestRate")
                        .HasColumnType("real");

                    b.Property<DateTime>("OpenDate")
                        .HasColumnType("datetime2");

                    b.HasKey("DepositId");

                    b.HasIndex("AccountId");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("BillingAPI.Models.Transactions", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("FromAccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ToAccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransactionId");

                    b.HasIndex("FromAccountId");

                    b.HasIndex("ToAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BillingAPI.Models.CustomerAccount", b =>
                {
                    b.HasOne("BillingAPI.Models.Account", "Account")
                        .WithMany("CustomerAccounts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BillingAPI.Models.Customer", "Customer")
                        .WithMany("CustomerAccounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BillingAPI.Models.Deposits", b =>
                {
                    b.HasOne("BillingAPI.Models.Account", "Account")
                        .WithMany("DepositsCollection")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BillingAPI.Models.Transactions", b =>
                {
                    b.HasOne("BillingAPI.Models.Account", "FromAccount")
                        .WithMany("TransactionsCollectionFrom")
                        .HasForeignKey("FromAccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BillingAPI.Models.Account", "ToAccount")
                        .WithMany("TransactionsCollectionTo")
                        .HasForeignKey("ToAccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FromAccount");

                    b.Navigation("ToAccount");
                });

            modelBuilder.Entity("BillingAPI.Models.Account", b =>
                {
                    b.Navigation("CustomerAccounts");

                    b.Navigation("DepositsCollection");

                    b.Navigation("TransactionsCollectionFrom");

                    b.Navigation("TransactionsCollectionTo");
                });

            modelBuilder.Entity("BillingAPI.Models.Customer", b =>
                {
                    b.Navigation("CustomerAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
