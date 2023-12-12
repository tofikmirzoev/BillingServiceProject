namespace BillingAPI.DTO;

public class CustomerDTO
{
    public string CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PlaceOfBirth { get; set; }
    public string CustomerType { get; set; }
}