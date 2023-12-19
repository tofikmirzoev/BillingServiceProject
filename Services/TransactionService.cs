using BillingAPI.BillingMessages;
using BillingAPI.Models;
using BillingAPI.Repository.UnitOfWork;
using BillingAPI.ServiceIntefaces;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BillingAPI.Services;

public class TransactionService : ITransactionService
{
    private UnitOfWork _unitOfWork { get; }
    public TransactionService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public bool DoPurchase(DoPurchaseRequest request, ModelStateDictionary modelState)
    {
        if (!_unitOfWork.Account.AccountExists(request.fromAccount) ||
            !_unitOfWork.Account.AccountExists(request.toAccount))
        {
            modelState.AddModelError("","Please check if the account is specified" +
                                        "correctly");
            return false;
        }
        
        var fromAccountObj = _unitOfWork.Account.GetAccount(request.fromAccount);
        var toAccountObj = _unitOfWork.Account.GetAccount(request.toAccount);
        
        if (fromAccountObj.AccountBalance - request.amountToSend > 0)
            fromAccountObj.AccountBalance -= request.amountToSend;
        else
            return false;
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

        var result = GenerateTransaction(new Transactions[] { senderTransaction, receiverTransaction });
        return result;
    }

    public bool MakeTopUp(TopUpRequest request, ModelStateDictionary modelState)
    {
        if (request.accountId == null)
        {
            modelState.AddModelError("","AccountId is Null");
            return false;
        }

        if (!_unitOfWork.Account.AccountExists(request.accountId))
        {
            modelState.AddModelError("","Please check if the account is specified" +
                                        "correctly");
            return false;
        }
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
        
        var result = GenerateTransaction(new Transactions[]{transaction});
        return result;
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