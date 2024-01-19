namespace BillingAPI.Models;

public class Deposits
{
    public string DepositID { get; set; }
    public double DepositBalance { get; set; }
    public uint DepositTerm { get; set; }
    public float InterestRate { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime CloseDate { get; set; }
    public string DepositStatus { get; set; }
    public Account Account { get; set; }
    public string AccountID { get; set; }
}