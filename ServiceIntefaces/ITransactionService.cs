using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.DTO;
using BillingAPI.Models;
using FluentResults;

namespace BillingAPI.ServiceIntefaces;

public interface ITransactionService
{
    public Result<TransactionDTO> DoPurchase(DoPurchaseRequest request);
    public Result MakeTopUp(TopUpRequest request);
    public Result<CollectionResponse> Collect(CollectionRequest request);
    public Result<TransactionDTO> CommitTransaction(CommitTransactionRequest request);
    public Result Disburse();
}