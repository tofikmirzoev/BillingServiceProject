using BillingAPI.BillingMessages;
using BillingAPI.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace BillingAPI.Interfaces;

public interface IDepositRepository
{
    public ICollection<Deposits> GetAllDeposits();
    public ICollection<Deposits> GetDeposits(string acocuntId);
    public Deposits GetDepositDetails(string depositId);
    public bool RegisterDeposit(RegisterDepositRequest registerDepositRequest);
    public bool UpdateDeposits(List<Deposits> deposits);
    public bool CloseDeposit(string depositId);
    public bool Save();
}