using BillingAPI.DTO;

namespace BillingAPI.BillingMessages;

public class AddCustomerRequest
{
    public CustomerDTO _customerDto { get; set; }
    public string accountId { get; set; }
    public double balance { get; set; }
}