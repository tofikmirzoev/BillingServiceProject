namespace BillingAPI.DTO;

public class TransactionDTO
{
    public int TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; }
    public double amount { get; set; }
    public double BalanceBefore { get; set; }
    public double BalanceAfter { get; set; }
}