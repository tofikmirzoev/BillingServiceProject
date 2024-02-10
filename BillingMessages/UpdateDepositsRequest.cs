using BillingAPI.DTO;

namespace BillingAPI.BillingMessages;

public class UpdateDepositsRequest
{
    public List<DepositDTO> DepositDtos { get; set; }
}