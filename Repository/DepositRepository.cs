using BillingAPI.BillingMessages;
using BillingAPI.Data;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingAPI.Repository;

public class DepositRepository : IDepositRepository
{
    private readonly DataContext _context;

    public DepositRepository (DataContext context)
    {
        _context = context;
    }

    public ICollection<Deposits> GetAllDeposits()
    {
        return _context.Deposits.OrderBy(d => d.OpenDate).ToList();
    }

    public ICollection<Deposits> GetDeposits(string acocuntId)
    {
        return _context.Deposits.Where(d => d.AccountId == acocuntId).OrderBy(de => de.DepositId).ToList();
    }

    public Deposits GetDepositDetails(string depositId)
    {
        return _context.Deposits.Where(d => d.DepositId == depositId).FirstOrDefault();
    }

    public bool RegisterDeposit(RegisterDepositRequest registerDepositRequest)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.AccountId == registerDepositRequest.AccountID);
        var depositToRegister = new Deposits()
        {
            Account = account,
            DepositBalance = registerDepositRequest.DepositAmount,
            InitialDepositBalance = registerDepositRequest.DepositAmount,
            DepositTerm = registerDepositRequest.DepositTerm,
            InterestRate = registerDepositRequest.InterestRate,
            OpenDate = registerDepositRequest.OpenDate,
            CloseDate = registerDepositRequest.CloseDate,
            DepositStatus = registerDepositRequest.DepositStatus
        };

        _context.Add(depositToRegister);
        return Save();
    }

    public bool UpdateDeposits(List<Deposits> deposits)
    {
        foreach (var deposit in deposits)
        {
            _context.Update(deposit);
        }

        return Save();
    }

    public bool CloseDeposit(string depositId)
    {
        var deposit = _context.Deposits.FirstOrDefault(d => d.DepositId == depositId);
        deposit.DepositStatus = "Closed";
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}