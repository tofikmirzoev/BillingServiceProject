using BillingAPI.Data;
using BillingAPI.Interfaces;
using BillingAPI.Models;

namespace BillingAPI.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly DataContext _context;

    public AccountRepository(DataContext context)
    {
        _context = context;
    }
    
    public ICollection<Account> GetAccounts()
    {
        return _context.Accounts.OrderBy(a => a.DateCreated).ToList();
    }

    public Account GetAccount(string accountId)
    {
        return _context.Accounts.Where(a => a.AccountId == accountId).FirstOrDefault() ?? throw new InvalidOperationException();
    }

    public bool AccountExists(string accountId)
    {
        return _context.Accounts.Any(a => a.AccountId == accountId);
    }
    
    public bool UpdateBalance(Account account, double newBalance)
    {
        account.AccountBalance = newBalance;
        return Save();
    }

    public bool AddAccount(Account account, string customerId)
    {
        var customer = _context.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault();
        if (customer == null)
            return false;

        var customerAccount = new CustomerAccount()
        {
            Account = account,
            Customer = customer
        };

        _context.Add(customerAccount);
        return Save();
    }

    public bool DeleteAccount(Account account)
    {
        _context.Accounts.Where(a => a.AccountId == account.AccountId).FirstOrDefault().Removed = true;
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
    
}