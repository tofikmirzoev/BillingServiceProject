using System.Runtime.InteropServices.JavaScript;
using BillingAPI.Data;
using BillingAPI.Interfaces;
using BillingAPI.Models;

namespace BillingAPI.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly DataContext _context;

    public CustomerRepository(DataContext context)
    {
        _context = context;
    }
    
    public ICollection<Customer> GetCustomers()
    {
        return _context.Customers.OrderBy(c => c.LastName).ToList();
    }

    public Customer GetCustomer(string customerId)
    {
        return _context.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault();
    }

    public bool CustomerExists(string customerId)
    {
        return _context.Customers.Any(c => c.CustomerId == customerId);
    }

    public bool AddCustomer(Customer customer, string accountId)
    {
        var customerAccount = new CustomerAccount()
        {
            Customer = customer,
            Account = new Account()
            {
                AccountId = accountId,
                AccountBalance = 0,
                DateCreated = DateTime.Now
            }
        };

        _context.Add(customerAccount);
        return Save();
    }
    
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}