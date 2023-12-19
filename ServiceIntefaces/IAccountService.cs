using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BillingAPI.ServiceIntefaces;

public interface IAccountService
{
    public ICollection<Account> GetAccounts();
    public Account GetAccount(string accountId, ModelStateDictionary modelState);
    public bool UpdateBalance(UpdateBalanceRequest request, ModelStateDictionary modelState);
    public RegisterAccountResponse RegisterAccountResponse(RegisterAccountRequest request);
}