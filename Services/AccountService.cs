using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.DTO;
using BillingAPI.Models;
using BillingAPI.Repository.UnitOfWork;
using BillingAPI.ServiceIntefaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BillingAPI.Services;

public class AccountService : IAccountService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccountService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
    }
    
    public ICollection<AccountDTO> GetAccounts()
    {
        return _mapper.Map<ICollection<AccountDTO>>(_unitOfWork.Account.GetAccounts());
    }

    public AccountDTO GetAccount(string accountId, ModelStateDictionary modelState)
    {
        if (!_unitOfWork.Account.AccountExists(accountId))
        {
            modelState.AddModelError("", "Account does not exists");
            return null;
        }
        return _mapper.Map<AccountDTO>(_unitOfWork.Account.GetAccount(accountId));
    }
    
    public bool UpdateBalance(UpdateBalanceRequest request, ModelStateDictionary modelState)
    {
        if (request.accountId == null || request.newBanalce < 0)
        {
            modelState.AddModelError("", "Wrong request, Please check request parameters");
            return false;
        }

        if (!_unitOfWork.Account.AccountExists(request.accountId))
        {
            modelState.AddModelError("", "Account does not exists");
            return false;
        }

        var account = _unitOfWork.Account.GetAccount(request.accountId);
        var updateBalanceTransaction = new Transactions()
        {
            TransactionDate = DateTime.Now,
            TransactionType = "Balance Update",
            amount = request.newBanalce,
            BalanceAfter = request.newBanalce,
            BalanceBefore = account.AccountBalance,
            Account = account
        };
        
        var updateBalanceResult =  _unitOfWork.Account.UpdateBalance(account, request.newBanalce);
        if (!updateBalanceResult)
        {
            modelState.AddModelError("", "Something went wrong");
            return false;
        }
        
        var transactionAddingResult = _unitOfWork.Tranasctions.AddTransaction(updateBalanceTransaction);
        if (!transactionAddingResult)
        {
            modelState.AddModelError("", "Transaction failed");
            return false;
        }

        return true;
    }
    
    public RegisterAccountResponse RegisterAccountResponse(RegisterAccountRequest request)
    {
        throw new NotImplementedException();
    }
}