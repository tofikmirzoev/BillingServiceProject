using System.Collections;
using System.Collections.ObjectModel;

namespace BillingAPI.Models;

public class Account
{
    public string AccountId { get; set; }
    public DateTime DateCreated{ get; set; }
    public double AccountBalance { get; set; }
    public ICollection<CustomerAccount> CustomerAccounts { get; set; }
    public ICollection<Transactions> TransactionsCollection { get; set; }
}