using BillingAPI.BillingMessages;
using BillingAPI.DTO;
using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BillingAPI.ServiceIntefaces;

public interface ICustomerService
{
    public ICollection<CustomerDTO> GetCustomers();
    public CustomerDTO GetCustomer(string customerId);
    public bool AddCustomer(AddCustomerRequest request, ModelStateDictionary modelState);
}