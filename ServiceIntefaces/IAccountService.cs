using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.DTO;
using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BillingAPI.ServiceIntefaces;

public interface IAccountService
{
    public ICollection<AccountDTO> GetAccounts();
    public AccountDTO GetAccount(string accountId, ModelStateDictionary modelState);
    public bool UpdateBalance(UpdateBalanceRequest request, ModelStateDictionary modelState);
    public RegisterAccountResponse RegisterAccountResponse(RegisterAccountRequest request);
}