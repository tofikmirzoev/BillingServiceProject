using BillingAPI.BillingMessages;
using FluentResults;

namespace BillingAPI.ServiceIntefaces;

public interface ITransactionService
{
    public Result DoPurchase(DoPurchaseRequest request);
    public Result MakeTopUp(TopUpRequest request);
}