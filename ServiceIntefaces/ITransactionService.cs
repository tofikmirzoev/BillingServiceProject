using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.DTO;
using BillingAPI.Models;
using FluentResults;

namespace BillingAPI.ServiceIntefaces;

public interface ITransactionService
{
    public Result<TransactionDTO> CommitTransaction(CommitTransactionRequest request);
    public Result Disburse();
}