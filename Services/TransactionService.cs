using BillingAPI.Models;
using BillingAPI.ServiceIntefaces;

namespace BillingAPI.Services;

public class TransactionService : ITransactionService
{
    public TransactionService()
    {
        
    }
    public List<Transactions> GetTransactions(string accountId)
    {
        throw new NotImplementedException();
    }

    public Transactions GetTransaction(int transactionId)
    {
        throw new NotImplementedException();
    }

    public bool AccountExists(string accountId)
    {
        throw new NotImplementedException();
    }

    public bool DoPurchase(string fromAccount, string toAccount, double amountToBeTransferred, string purchaseType)
    {
        throw new NotImplementedException();
    }

    public bool MakeTopUp(string accountId, double amount)
    {
        throw new NotImplementedException();
    }

    public bool Save()
    {
        throw new NotImplementedException();
    }
}