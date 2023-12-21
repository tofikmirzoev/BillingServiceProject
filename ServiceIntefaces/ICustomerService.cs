using BillingAPI.BillingMessages;
using BillingAPI.DTO;
using BillingAPI.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BillingAPI.ServiceIntefaces;

public interface ICustomerService
{
    public Result<ICollection<CustomerDTO>> GetCustomers();
    public Result<CustomerDTO> GetCustomer(string customerId);
    public Result AddCustomer(AddCustomerRequest request, ModelStateDictionary modelState);
}