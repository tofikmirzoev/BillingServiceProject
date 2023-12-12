using BillingAPI.Models;

namespace BillingAPI.Interfaces;

public interface IAccountRepository
{
    public ICollection<Account> GetAccounts();
    public Account GetAccount(string accountId);
    public bool AccountExists(string accountId);
    public bool UpdateBalance(string accountId, double newBalance);
    public bool AddAccount(Account account, string customerId);
    public bool Save();
}