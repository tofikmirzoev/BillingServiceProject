using System.ComponentModel;
using BillingAPI.Data;
using BillingAPI.Models;

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
                        CustomerType = "Private",
                    },
                    Account = new Account()
                    {
                        AccountId = "tofik01",
                        DateCreated = new DateTime(2020, 01, 11),
                        AccountBalance = 6000,
                        TransactionsCollection = new List<Transactions>()
                        {
                            new Transactions()
                            {
                                TransactionDate = DateTime.Now,
                                TransactionType = "Store purchase",
                                amount = 15,
                                BalanceBefore = 100,
                                BalanceAfter = 85
                            },
                            new Transactions()
                            {
                                TransactionDate = DateTime.Now + TimeSpan.FromHours(1),
                                TransactionType = "Utility purchase",
                                amount = 45,
                                BalanceBefore = 85,
                                BalanceAfter = 40
                            }
                        }
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
                        CustomerType = "Private",
                    },
                    Account = new Account()
                    {
                        AccountId = "inara01",
                        DateCreated = new DateTime(2021, 11, 6),
                        AccountBalance = 3500,
                        TransactionsCollection = new List<Transactions>()
                        {
                            new Transactions()
                            {
                                TransactionDate = DateTime.Now + TimeSpan.FromHours(2),
                                TransactionType = "Online purchase",
                                amount = 32,
                                BalanceBefore = 200,
                                BalanceAfter = 168
                            },
                            new Transactions()
                            {
                                TransactionDate = DateTime.Now + TimeSpan.FromHours(4),
                                TransactionType = "Online purchase",
                                amount = 10,
                                BalanceBefore = 168,
                                BalanceAfter = 158
                            }
                        }
                    }
                }
            };
            _context.CustomerAccounts.AddRange(customerAccounts);
            _context.SaveChanges();
        }
    }
}