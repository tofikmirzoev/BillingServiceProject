using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.Models;
using BillingAPI.Repository.UnitOfWork;
using BillingAPI.ServiceIntefaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BillingAPI.Services;

public class AccountService : IAccountService
{
    private UnitOfWork _unitOfWork { get; }

    public AccountService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public ICollection<Account> GetAccounts()
    {
        return _unitOfWork.Account.GetAccounts();
    }

    public Account GetAccount(string accountId, ModelStateDictionary modelState)
    {
        if (!_unitOfWork.Account.AccountExists(accountId))
        {
            modelState.AddModelError("", "Account does not exists");
            return null;
        }
        return _unitOfWork.Account.GetAccount(accountId);
    }
    
    public bool UpdateBalance(UpdateBalanceRequest request, ModelStateDictionary modelState)
    {
        if (request.accountId == null || request.newBanalce < 0)
        {
            modelState.AddModelError("", "Wrong request, Please check request parameter");
            return false;
        }

        if (!_unitOfWork.Account.AccountExists(request.accountId))
        {
            modelState.AddModelError("", "Account does not exists");
            return false;
        }

        var account = _unitOfWork.Account.GetAccount(request.accountId);
        return _unitOfWork.Account.UpdateBalance(account, request.newBanalce);
    }

    public RegisterAccountResponse RegisterAccountResponse(RegisterAccountRequest request)
    {
        throw new NotImplementedException();
    }
}