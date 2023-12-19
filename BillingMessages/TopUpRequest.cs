namespace BillingAPI.BillingMessages;

public class TopUpRequest
{
    public string accountId { get; set; }
    public double topUpAmount { get; set; }
}