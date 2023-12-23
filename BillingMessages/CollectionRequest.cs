namespace BillingAPI.BillingMessages;

public class CollectionRequest
{
    public string accountId { get; set; } // account from
    public string creditAccountId { get; set; } // account to
    public double ammountToCollect { get; set; }
}