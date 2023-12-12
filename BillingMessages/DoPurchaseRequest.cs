namespace BillingAPI.BillingMessages;

public class DoPurchaseRequest
{
    public string fromAccount { get; set; }
    public string toAccount { get; set; }
    public double amountToSend { get; set; }
    public string purchaseType { get; set; }
}