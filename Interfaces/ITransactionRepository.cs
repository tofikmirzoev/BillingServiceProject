using BillingAPI.Models;

namespace BillingAPI.Interfaces;

public interface ITransactionRepository
{
    public List<Transactions> GetTransactions(string accountId);
    public Transactions GetTransaction(int transactionId);
    public bool AccountExists(string accountId);
    public bool DoPurchase(string fromAccount, string toAccount, double amountToBeTransferred, string purchaseType);
    public bool MakeTopUp(string accountId, double amount);
    public bool Save();
}