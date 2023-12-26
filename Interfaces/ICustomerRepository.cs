using BillingAPI.Models;

namespace BillingAPI.Interfaces;

public interface ICustomerRepository
{
    public ICollection<Customer> GetCustomers();
    public Customer GetCustomer(string customerId);
    public bool CustomerExists(string customerId);
    public bool Save();
    public bool AddCustomer(Customer customer, string accountId, double balance); 
    public bool DeleteCustomer(Customer customer); 
}