using BillingAPI.BillingMessages;
using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BillingAPI.ServiceIntefaces;

public interface ITransactionService
{
    public bool DoPurchase(DoPurchaseRequest request, ModelStateDictionary modelState);
    public bool MakeTopUp(string accountId, double amount);
}