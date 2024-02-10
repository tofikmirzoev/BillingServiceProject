using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.DTO;
using FluentResults;

namespace BillingAPI.ServiceIntefaces;

public interface IAccountService
{
    public Result<ICollection<AccountDTO>> GetAccounts();
    public Result<ICollection<AccountDTO>> GetAllAccounts();
    public Result<ICollection<AccountDTO>> GetAllAccountsWithDeposit();
    public Result<AccountDTO> GetAccount(string accountId);
    public Result UpdateBalance(UpdateBalanceRequest request);
    public RegisterAccountResponse RegisterAccountResponse(RegisterAccountRequest request);
    public Result RemoveAccount(string accountId);
    public Result RecoverAccount(string accountId);
}