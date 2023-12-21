using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.DTO;
using BillingAPI.Models;
using BillingAPI.Repository.UnitOfWork;
using BillingAPI.ServiceIntefaces;
using FluentResults;
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
    public Result<ICollection<CustomerDTO>> GetCustomers()
    {
        return Result.Ok(_mapper.Map<ICollection<CustomerDTO>>(_unitOfWork.Customer.GetCustomers()));
    }

    public Result<CustomerDTO> GetCustomer(string customerId)
    {
        if(_unitOfWork.Customer.CustomerExists(customerId))
            return Result.Fail<CustomerDTO>("Customer does not exists");

        var customerDto = _mapper.Map<CustomerDTO>(_unitOfWork.Customer.GetCustomer(customerId));
        return Result.Ok(customerDto);
    }

    public Result AddCustomer(AddCustomerRequest request,ModelStateDictionary modelState)
    {
        if (request.accountId == null || request._customerDto == null)
            return Result.Fail("Wrong request, Please check request parameters");

        if (_unitOfWork.Customer.CustomerExists(request._customerDto.CustomerId))
            return Result.Fail("Customer already exist");

        if (_unitOfWork.Account.AccountExists(request.accountId))
            return Result.Fail("Account already exist");

        var customer = _mapper.Map<Customer>(request._customerDto);
        var addCustomerResult = _unitOfWork.Customer.AddCustomer(customer, request.accountId, request.balance);
        if (!addCustomerResult)
            return Result.Fail("Something went wrong");

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
            return Result.Fail("Transaction failed");

        return Result.Ok();
    }
}