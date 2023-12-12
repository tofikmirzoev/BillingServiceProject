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
        return _context.Transactions.Where(t => t.Account.AccountId == accountId).ToList();
    }

    public Transactions GetTransaction(int transactionId)
    {
        return _context.Transactions.Where(t => t.TransactionId == transactionId).FirstOrDefault();
    }
    
    public bool AccountExists(string accountId)
    {
        return _context.Accounts.Any(a => a.AccountId == accountId);
    }
    
    public bool GenerateTransaction(Transactions[] transactions)
    {
        for (int i = 0; i < transactions.Length; i++)
        {
            _context.Transactions.Add(transactions[i]);
        }
        return Save();
    }
    
    public bool DoPurchase(string fromAccount, string toAccount, double amountToBeTransfered, string purchaseType)
    {
        var fromAccountObj = _context.Accounts.Where(a => a.AccountId == fromAccount).FirstOrDefault();
        var toAccountObj = _context.Accounts.Where(a => a.AccountId == toAccount).FirstOrDefault();

        // if (fromAccountObj == null || toAccountObj == null)
        //     return false;

        if (fromAccountObj.AccountBalance - amountToBeTransfered > 0)
            fromAccountObj.AccountBalance -= amountToBeTransfered;
        else
            return false;
        
        toAccountObj.AccountBalance += amountToBeTransfered;
        var senderTransaction = new Transactions()
        {
            TransactionDate = DateTime.Now,
            TransactionType = purchaseType,
            amount = amountToBeTransfered,
            BalanceAfter = fromAccountObj.AccountBalance,
            BalanceBefore = fromAccountObj.AccountBalance + amountToBeTransfered,
            Account = fromAccountObj
        };
        
        var receiverTransaction = new Transactions()
        {
            TransactionDate = DateTime.Now,
            TransactionType = purchaseType,
            amount = amountToBeTransfered,
            BalanceAfter = toAccountObj.AccountBalance,
            BalanceBefore = toAccountObj.AccountBalance - amountToBeTransfered,
            Account = toAccountObj
        };
        
        var result = GenerateTransaction(new Transactions[]{senderTransaction,receiverTransaction});
        return result;
    }
    
    public bool MakeTopUp(string accountId, double amount)
    {
        if (accountId == null)
            return false;

        var accountObj = _context.Accounts.Where(a => a.AccountId == accountId).FirstOrDefault();
        accountObj.AccountBalance += amount;

        var transaction = new Transactions()
        {
            TransactionDate = DateTime.Now,
            TransactionType = "Top Up",
            amount = amount,
            BalanceAfter = accountObj.AccountBalance,
            BalanceBefore = accountObj.AccountBalance - amount,
            Account = accountObj
        };
        
        var result = GenerateTransaction(new Transactions[]{transaction});
        return result;
    }
    
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}