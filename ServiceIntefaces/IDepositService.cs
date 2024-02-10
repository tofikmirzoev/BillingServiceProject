using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.DTO;
using FluentResults;

namespace BillingAPI.ServiceIntefaces;

public interface IDepositService
{
    Result<ICollection<DepositDTO>> GetAllDeposits();
    Result<ICollection<DepositDTO>> GetDeposits(string accountId);
    Result<DepositDTO> GetDepositDetails(string depositId);
    Result RegisterDeposit(RegisterDepositRequest request);
    Result<UpdateDepositResponse> UpdateDeposits(UpdateDepositsRequest request);
    Result CloseDeposit(string depositId);
}