namespace BillingAPI.BillingMessages;

public class UpdateBalanceRequest
{
    public string accountId { get; set; }
    public double newBanalce { get; set; }
}