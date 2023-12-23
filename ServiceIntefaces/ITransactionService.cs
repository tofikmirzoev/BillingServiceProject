using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using FluentResults;

namespace BillingAPI.ServiceIntefaces;

public interface ITransactionService
{
    public Result DoPurchase(DoPurchaseRequest request);
    public Result MakeTopUp(TopUpRequest request);
    public Result<CollectionResponse> Collect(CollectionRequest request);
    public Result Disburse();
}