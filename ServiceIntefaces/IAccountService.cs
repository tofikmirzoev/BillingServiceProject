using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.DTO;
using BillingAPI.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BillingAPI.ServiceIntefaces;

public interface IAccountService
{
    public Result<ICollection<AccountDTO>> GetAccounts();
    public Result<AccountDTO> GetAccount(string accountId);
    public Result UpdateBalance(UpdateBalanceRequest request);
    public RegisterAccountResponse RegisterAccountResponse(RegisterAccountRequest request);
    public Result RemoveAccount(string accountId);
}