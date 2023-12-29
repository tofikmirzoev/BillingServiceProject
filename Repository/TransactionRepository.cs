using System.Transactions;
using BillingAPI.Data;
using BillingAPI.Interfaces;
using BillingAPI.Models;

namespace BillingAPI.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly DataContext _context;

    public TransactionRepository(DataContext context)
    {
        _context = context;
    }
    
    public List<Transactions> GetTransactions(string accountId)
    {
        return _context.Transactions.Where(t => t.FromAccount.AccountId == accountId).ToList();
    }

    public Transactions GetTransaction(int transactionId)
    {
        return _context.Transactions.Where(t => t.TransactionId == transactionId).FirstOrDefault();
    }
    
    public bool AccountExists(string accountId)
    {
        return _context.Accounts.Any(a => a.AccountId == accountId);
    }


    public bool AddTransaction(Transactions transaction)
    {
        _context.Add(transaction);
        return Save();
    }
    
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}