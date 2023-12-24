using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.DTO;
using BillingAPI.Models;
using BillingAPI.Repository.UnitOfWork;
using BillingAPI.ServiceIntefaces;
using FluentResults;

namespace BillingAPI.Services;

public class AccountService : IAccountService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccountService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Result<ICollection<AccountDTO>> GetAccounts()
    {
        return Result.Ok(_mapper.Map<ICollection<AccountDTO>>(_unitOfWork.Account.GetAccounts()));
    }

    public Result<AccountDTO> GetAccount(string accountId)
    {
        if (!_unitOfWork.Account.AccountExists(accountId))
            return Result.Fail<AccountDTO>("Account does not exists");

        var accountDto = _mapper.Map<AccountDTO>(_unitOfWork.Account.GetAccount(accountId));
        return Result.Ok(accountDto);
    }
    
    public Result UpdateBalance(UpdateBalanceRequest request)
    {
        if (request.accountId == null || request.newBanalce < 0)
            return Result.Fail("Account Id or Amount is wrong");

        if (!_unitOfWork.Account.AccountExists(request.accountId))
            return Result.Fail("Account does not exists");
        
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
            return Result.Fail("Something went wrong");
        }
        
        var transactionAddingResult = _unitOfWork.Tranasctions.AddTransaction(updateBalanceTransaction);
        if (!transactionAddingResult)
        {
            return Result.Fail("Transaction failed");
        }

        return Result.Ok();
    }
    
    public RegisterAccountResponse RegisterAccountResponse(RegisterAccountRequest request)
    {
        throw new NotImplementedException();
    }

    public Result RemoveAccount(string accountId)
    {
        if (!_unitOfWork.Account.AccountExists(accountId))
            return Result.Fail("Account does not exist");
        
        var account = _unitOfWork.Account.GetAccount(accountId);
        _unitOfWork.Account.DeleteAccount(account);
        return Result.Ok();
    }
}