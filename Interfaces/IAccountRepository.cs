using BillingAPI.Models;

namespace BillingAPI.Interfaces;

public interface IAccountRepository
{
    public ICollection<Account> GetAccounts();
    public ICollection<Account> GetAllAccounts();
    public Account GetAccount(string accountId);
    public Account GetRemovedAccount(string accountId);
    public bool AccountExists(string accountId);
    public bool IfAccountRemoved(string accountId);
    public bool UpdateBalance(Account account, double newBalance);
    public bool AddAccount(Account account, string customerId);
    public bool DeleteAccount(Account accountId);
    public bool RecoverAccount(Account accountId);
    public bool Save();
}