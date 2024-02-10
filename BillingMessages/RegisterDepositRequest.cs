namespace BillingAPI.BillingMessages;

public class RegisterDepositRequest
{
    public string AccountID { get; set; }
    public double DepositAmount { get; set; }
    public uint DepositTerm { get; set; }
    public float InterestRate { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime CloseDate { get; set; }
    public string DepositStatus { get; set; }
}