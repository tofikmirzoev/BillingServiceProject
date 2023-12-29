using System.ComponentModel;
using BillingAPI.Data;
using BillingAPI.Models;
using DateTime = System.DateTime;

namespace BillingAPI;

public class DataSeeder
{
    private DataContext _context;
    public DataSeeder(DataContext context)
    {
        _context = context;
    }

    public void SeedDataContext()
    {
        if (!_context.CustomerAccounts.Any())
        {
            var customerAccounts = new List<CustomerAccount>()
            {
                new CustomerAccount()
                {
                    Customer = new Customer()
                    {
                        CustomerId = "000001",
                        FirstName = "Tofik",
                        LastName = "Mirzayev",
                        DateOfBirth = new DateTime(1998, 11, 24),
                        PlaceOfBirth = "Baku",
                        CustomerType = "Private"
                    },
                    Account = new Account()
                    {
                        AccountId = "tofik01",
                        DateCreated = new DateTime(2020, 01, 11),
                        AccountBalance = 6000,
                        Removed = false
                    }
                },
                new CustomerAccount()
                {
                    Customer = new Customer()
                    {
                        CustomerId = "000002",
                        FirstName = "Inara",
                        LastName = "Aghayeva",
                        DateOfBirth = new DateTime(1999, 11, 10),
                        PlaceOfBirth = "Baku",
                        CustomerType = "Private"
                    },
                    Account = new Account()
                    {
                        AccountId = "inara01",
                        DateCreated = new DateTime(2021, 11, 6),
                        AccountBalance = 3500,
                        Removed = false
                    }
                }
            };
            _context.CustomerAccounts.AddRange(customerAccounts);
            _context.SaveChanges();
        }
    }
}