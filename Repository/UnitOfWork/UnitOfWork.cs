using BillingAPI.Data;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.ServiceIntefaces;
using Microsoft.EntityFrameworkCore;

namespace BillingAPI.Repository.UnitOfWork;

public class UnitOfWork : IDisposable
{
    private bool disposed = false;
    private DataContext _context;
    private IAccountRepository _accountRepository;
    private ICustomerRepository _customerRepository;
    private ITransactionRepository _transactionRepository;

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
    public IAccountRepository Account
    {
        get
        {
            if (_accountRepository == null)
                _accountRepository = new AccountRepository(_context);
            
            return _accountRepository;
        }
    }
    public ICustomerRepository Customer
    {
        get
        {
            if (_customerRepository == null)
                _customerRepository = new CustomerRepository(_context);
            
            return _customerRepository;
        }
    }
    public ITransactionRepository Tranasctions
    {
        get
        {
            if (_transactionRepository == null)
                _transactionRepository = new TransactionRepository(_context);
            
            return _transactionRepository;
        }
    }
    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
                _context.Dispose();

            this.disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}