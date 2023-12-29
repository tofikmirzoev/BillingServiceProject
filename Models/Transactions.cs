namespace BillingAPI.Models;

public class Transactions
{
    public int TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; }
    public double Amount { get; set; }
    public string FromAccountId { get; set; }
    public string ToAccountId { get; set; }
    public Account FromAccount { get; set; }
    public Account ToAccount { get; set; }
}