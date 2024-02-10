namespace BillingAPI.DTO;

public class DepositDTO
{
    public string DepositId { get; set; }
    public double DepositBalance { get; set; }
    public double InitialDepositBalance { get; set; }
    public uint DepositTerm { get; set; }
    public float InterestRate { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime CloseDate { get; set; }
    public string DepositStatus { get; set; }
}