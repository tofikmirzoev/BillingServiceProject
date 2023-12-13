using BillingAPI.Data;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.ServiceIntefaces;
using Microsoft.EntityFrameworkCore;

namespace BillingAPI.Repository.UnitOfWork;

public class UnitOfWork : IDisposable
{
    private bool disposed = false;
    private static DbContextOptions<DataContext> options;
    private DataContext _context = new DataContext(options);
    private IAccountRepository _accountRepository;
    private ICustomerRepository _customerRepository;
    private ITransactionRepository _transactionRepository;

    public IAccountRepository account
    {
        get
        {
            if (_accountRepository == null)
            {
                _accountRepository = new AccountRepository(_context);
            }
            return _accountRepository;
        }
    }
    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            this.disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}