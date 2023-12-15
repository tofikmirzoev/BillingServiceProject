using BillingAPI.Models;

namespace BillingAPI.Interfaces;

public interface ITransactionRepository
{
    public List<Transactions> GetTransactions(string accountId);
    public Transactions GetTransaction(int transactionId);
    public bool AccountExists(string accountId);
    public bool AddTransaction(Transactions transaction);
    public bool Save();
}