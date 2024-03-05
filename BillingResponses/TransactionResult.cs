namespace BillingAPI.BillingResponses;

public class TransactionResult
{
    public string resultCode { get; set; }
    public string resultDescription { get; set; }
    public double amount { get; set; }
}