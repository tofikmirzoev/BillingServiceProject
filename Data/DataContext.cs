using System.Diagnostics;

namespace BillingAPI.Data;
using Microsoft.EntityFrameworkCore;
using BillingAPI.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<CustomerAccount> CustomerAccounts { get; set; }
    public DbSet<Transactions> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerAccount>().HasKey(ca => new { ca.CustomerId, ca.AccountId });
        modelBuilder.Entity<CustomerAccount>().HasOne(c => c.Customer).WithMany(cs => cs.CustomerAccounts)
            .HasForeignKey(fk => fk.CustomerId);
        modelBuilder.Entity<CustomerAccount>().HasOne(a => a.Account).WithMany(acs => acs.CustomerAccounts)
            .HasForeignKey(fk => fk.AccountId);

        modelBuilder.Entity<Transactions>().HasKey(tr => new { tr.TransactionId });

        modelBuilder.Entity<Transactions>().HasOne(tr => tr.FromAccount).WithMany(a => a.TransactionsCollectionFrom)
            .HasForeignKey(tfk => tfk.FromAccountId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Transactions>().HasOne(tr => tr.ToAccount).WithMany(a => a.TransactionsCollectionTo)
            .HasForeignKey(tfk => tfk.ToAccountId).OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Deposits>().HasKey(d => new { d.DepositID });

        modelBuilder.Entity<Deposits>().HasOne(d => d.Account).WithMany(a => a.DepositsCollection)
            .HasForeignKey(dfk => dfk.AccountID).OnDelete(DeleteBehavior.NoAction);
    }
}