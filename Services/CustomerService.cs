using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.DTO;
using BillingAPI.Models;
using BillingAPI.Repository.UnitOfWork;
using BillingAPI.ServiceIntefaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BillingAPI.Services;

public class CustomerService : ICustomerService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public ICollection<CustomerDTO> GetCustomers()
    {
        return _mapper.Map<ICollection<CustomerDTO>>(_unitOfWork.Customer.GetCustomers());
    }

    public CustomerDTO GetCustomer(string customerId)
    {
        return _mapper.Map<CustomerDTO>(_unitOfWork.Customer.GetCustomer(customerId));
    }

    public bool AddCustomer(AddCustomerRequest request,ModelStateDictionary modelState)
    {
        if (request.accountId == null || request._customerDto == null)
        {
            modelState.AddModelError("", "Wrong request, Please check request parameters");
            return false;
        }

        if (_unitOfWork.Customer.CustomerExists(request._customerDto.CustomerId))
        {
            modelState.AddModelError("","Customer is already exist");
            return false;
        }

        if (_unitOfWork.Account.AccountExists(request.accountId))
        {
            modelState.AddModelError("","Account is already exist");
            return false;
        }

        var customer = _mapper.Map<Customer>(request._customerDto);
        var addCustomerResult = _unitOfWork.Customer.AddCustomer(customer, request.accountId, request.balance);
        if (!addCustomerResult)
        {
            modelState.AddModelError("", "Something went wrong");
            return false;
        }

        var accountRegistrationTransaction = new Transactions()
        {
            TransactionDate = DateTime.Now,
            TransactionType = "Account balance top up on creat",
            amount = request.balance,
            BalanceAfter = request.balance,
            BalanceBefore = 0,
            Account = _unitOfWork.Account.GetAccount(request.accountId)
        };

        var transactionAddingResult = _unitOfWork.Tranasctions.AddTransaction(accountRegistrationTransaction);
        if (!transactionAddingResult)
        {
            modelState.AddModelError("", "Transaction failed");
            return false;
        }

        return true;
    }
}