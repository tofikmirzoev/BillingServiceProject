using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.DTO;
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
    
    public Result<TransactionDTO> DoPurchase(DoPurchaseRequest request)
    {
        if (!_unitOfWork.Account.AccountExists(request.fromAccount) ||
            !_unitOfWork.Account.AccountExists(request.toAccount))
            return Result.Fail<TransactionDTO>("Please check if the account is specified" +
                               "correctly");
        
        var fromAccountObj = _unitOfWork.Account.GetAccount(request.fromAccount);
        var toAccountObj = _unitOfWork.Account.GetAccount(request.toAccount);
        
        if (fromAccountObj.AccountBalance - request.amountToSend > 0)
            fromAccountObj.AccountBalance -= request.amountToSend;
        else
            return Result.Fail<TransactionDTO>("Not enough balance");
        
        toAccountObj.AccountBalance += request.amountToSend;
        var senderTransaction = new Transactions()
        {
            TransactionDate = DateTime.Now,
            TransactionType = request.purchaseType,
            Amount = request.amountToSend,
            FromAccount = fromAccountObj,
            ToAccount = toAccountObj
        };
        
        if (!GenerateTransaction(new Transactions[] { senderTransaction }))
            return Result.Fail<TransactionDTO>("Transaction failed");
        
        return Result.Ok(_mapper.Map<TransactionDTO>(senderTransaction));
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
            Amount = request.topUpAmount,
            FromAccount = accountObj
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
            return Result.Ok(new CollectionResponse(){resultCode = 2, resultDescribtion = "Account does not exists", collectedAmount = 0});

        var accountToCredit = _unitOfWork.Account.GetAccount(request.accountId);
        var creditAccount = _unitOfWork.Account.GetAccount(request.creditAccountId);

        var amountToCollect = CalculateAmountToCollect(accountToCredit.AccountBalance, request.ammountToCollect);
        if (amountToCollect == 0)
            return Result.Ok(new CollectionResponse(){resultCode = 1, resultDescribtion = "Not enough money", collectedAmount = 0});
        else
        {
            accountToCredit.AccountBalance -= amountToCollect;
            creditAccount.AccountBalance += amountToCollect;
        }
        
        var collectionTransaction = new Transactions()
        {
            TransactionDate = DateTime.Now,
            TransactionType = "Collection",
            Amount = amountToCollect,
            FromAccount = accountToCredit,
            ToAccount = creditAccount
        };


        var transactionResult = GenerateTransaction(new Transactions[] { collectionTransaction });
        if (!transactionResult)
            return Result.Fail<CollectionResponse>("Transaction failed");

        return Result.Ok(new CollectionResponse(){resultCode = 0, resultDescribtion = "Successful", collectedAmount = amountToCollect});
    }

    public Result<TransactionDTO> CommitTransaction(CommitTransactionRequest request)
    {
        if (!_unitOfWork.Account.AccountExists(request.fromAccount) ||
            !_unitOfWork.Account.AccountExists(request.toAccount))
            return Result.Fail<TransactionDTO>("Please check if the account is specified" +
                                               "correctly");
        
        var fromAccountObj = _unitOfWork.Account.GetAccount(request.fromAccount);
        var toAccountObj = _unitOfWork.Account.GetAccount(request.toAccount);
        
        if (fromAccountObj.AccountBalance - request.amountToSend > 0)
            fromAccountObj.AccountBalance -= request.amountToSend;
        else
            return Result.Fail<TransactionDTO>("Not enough balance");
        
        toAccountObj.AccountBalance += request.amountToSend;
        var senderTransaction = new Transactions()
        {
            TransactionDate = DateTime.Now,
            TransactionType = request.transactionType,
            Amount = request.amountToSend,
            FromAccount = fromAccountObj,
            ToAccount = toAccountObj
        };
        
        if (!GenerateTransaction(new Transactions[] { senderTransaction }))
            return Result.Fail<TransactionDTO>("Transaction failed");
        
        return Result.Ok(_mapper.Map<TransactionDTO>(senderTransaction));
    }

    public Result Disburse()
    {
        throw new NotImplementedException();
    }

    private double CalculateAmountToCollect(double accountBalance, double maxAmountToCollect)
    {
        if (accountBalance == 0)
            return 0;
        
        if (accountBalance >= maxAmountToCollect)
            return maxAmountToCollect;

        var amountDifference = maxAmountToCollect - accountBalance;
        return maxAmountToCollect - amountDifference;
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