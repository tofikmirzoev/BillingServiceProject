using System.Collections;
using System.Collections.ObjectModel;

namespace BillingAPI.Models;

public class Account
{
    public string AccountId { get; set; }
    public DateTime DateCreated{ get; set; }
    public double AccountBalance { get; set; }
    public bool Removed { get; set; }
    public string AccountType { get; set; }
    public ICollection<CustomerAccount> CustomerAccounts { get; set; }
    public ICollection<Transactions> TransactionsCollectionTo { get; set; }
    public ICollection<Transactions> TransactionsCollectionFrom { get; set; }
    public ICollection<Deposits> DepositsCollection { get; set; }
}