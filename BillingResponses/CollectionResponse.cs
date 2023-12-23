namespace BillingAPI.BillingResponses;

public class CollectionResponse
{
    public double collectedAmount { get; set; }
    public int resultCode { get; set; }
    public string resultDescribtion { get; set; }
}