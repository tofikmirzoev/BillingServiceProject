using BillingAPI.Models;
using BillingAPI.Repository.UnitOfWork;
using BillingAPI.ServiceIntefaces;

namespace BillingAPI.Services;

public class TransactionService : ITransactionService
{
    private UnitOfWork _unitOfWork { get; }
    public TransactionService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        return _unitOfWork.account.AccountExists(accountId);
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