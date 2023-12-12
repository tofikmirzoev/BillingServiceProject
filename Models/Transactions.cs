namespace BillingAPI.Models;

public class Transactions
{
    public int TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; }
    public double amount { get; set; }
    public double BalanceBefore { get; set; }
    public double BalanceAfter { get; set; }
    public Account Account { get; set; }
}