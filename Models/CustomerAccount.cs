namespace BillingAPI.Models;

public class CustomerAccount
{
    public string CustomerId { get; set; }
    public string AccountId { get; set; }
    public Customer Customer { get; set; }
    public Account Account { get; set; }
    
}