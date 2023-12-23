using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.Models;
using BillingAPI.Repository.UnitOfWork;
using BillingAPI.ServiceIntefaces;
using FluentResults;

namespace BillingAPI.Services;

public class TransactionService : ITransactionService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TransactionService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Result DoPurchase(DoPurchaseRequest request)
    {
        if (!_unitOfWork.Account.AccountExists(request.fromAccount) ||
            !_unitOfWork.Account.AccountExists(request.toAccount))
            return Result.Fail("Please check if the account is specified" +
                               "correctly");
        
        var fromAccountObj = _unitOfWork.Account.GetAccount(request.fromAccount);
        var toAccountObj = _unitOfWork.Account.GetAccount(request.toAccount);
        
        if (fromAccountObj.AccountBalance - request.amountToSend > 0)
            fromAccountObj.AccountBalance -= request.amountToSend;
        else
            return Result.Fail("Not enough balance");
        
        toAccountObj.AccountBalance += request.amountToSend;
        var senderTransaction = new Transactions()
        {
            TransactionDate = DateTime.Now,
            TransactionType = request.purchaseType,
            amount = request.amountToSend,
            BalanceAfter = fromAccountObj.AccountBalance,
            BalanceBefore = fromAccountObj.AccountBalance + request.amountToSend,
            Account = fromAccountObj
        };
        
        var receiverTransaction = new Transactions()
        {
            TransactionDate = DateTime.Now,
            TransactionType = request.purchaseType,
            amount = request.amountToSend,
            BalanceAfter = toAccountObj.AccountBalance,
            BalanceBefore = toAccountObj.AccountBalance - request.amountToSend,
            Account = toAccountObj
        };

        if (!GenerateTransaction(new Transactions[] { senderTransaction, receiverTransaction }))
            return Result.Fail("Transaction failed");
        
        return Result.Ok();
    }

    public Result MakeTopUp(TopUpRequest request)
    {
        if (request.accountId == null)
            return Result.Fail("AccountId is Null");
        
        if (!_unitOfWork.Account.AccountExists(request.accountId))
            return Result.Fail("Please check if the account is specified" +
                               "correctly");
        
        var accountObj = _unitOfWork.Account.GetAccount(request.accountId);
        accountObj.AccountBalance += request.topUpAmount;

        var transaction = new Transactions()
        {
            TransactionDate = DateTime.Now,
            TransactionType = "Top Up",
            amount = request.topUpAmount,
            BalanceAfter = accountObj.AccountBalance,
            BalanceBefore = accountObj.AccountBalance - request.topUpAmount,
            Account = accountObj
        };

        if (!GenerateTransaction(new Transactions[] { transaction }))
            return Result.Fail("Transaction failed");
        
        return Result.Ok();
    }

    public Result<CollectionResponse> Collect(CollectionRequest request)
    {
        if (request.accountId == null || request.creditAccountId == null ||
            request.ammountToCollect <= 0)
            return Result.Fail("Values can not be Null");

        if (!_unitOfWork.Account.AccountExists((request.accountId)) ||
            !_unitOfWork.Account.AccountExists(request.creditAccountId))
            return Result.Fail<CollectionResponse>("Account does not exist");

        var accounttoCredit = _unitOfWork.Account.GetAccount(request.accountId);
        var creditAccount = _unitOfWork.Account.GetAccount(request.creditAccountId);
        
        
    }

    public Result Disburse()
    {
        throw new NotImplementedException();
    }

    private bool GenerateTransaction(Transactions[] transactions)
    {
        for (int i = 0; i < transactions.Length; i++)
        {
            if (!_unitOfWork.Tranasctions.AddTransaction(transactions[i]))
                return false;
        }
        return true;
    }
}