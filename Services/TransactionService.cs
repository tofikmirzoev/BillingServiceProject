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

    public Result<TransactionDTO> CommitTransaction(CommitTransactionRequest request)
    {
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