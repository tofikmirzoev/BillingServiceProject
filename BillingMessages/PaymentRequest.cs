namespace BillingAPI.BillingMessages;

public class PaymentRequest
{
    public string fromAccount { get; set; }
    public string toAccount { get; set; }
    public double amountToSend { get; set; }
    public string transactionType { get; set; }

}