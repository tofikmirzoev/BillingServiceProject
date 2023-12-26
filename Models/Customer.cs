namespace BillingAPI.Models;

public class Customer
{
    public string CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PlaceOfBirth { get; set; }
    public string CustomerType { get; set; }
    public ICollection<CustomerAccount> CustomerAccounts { get; set; }
}