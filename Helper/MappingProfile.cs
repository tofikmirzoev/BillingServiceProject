using AutoMapper;
using BillingAPI.DTO;
using BillingAPI.Models;
using BillingAPI.Repository;

namespace BillingAPI.Helper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDTO>();
        CreateMap<CustomerDTO, Customer>();
        CreateMap<Account, AccountDTO>();
        CreateMap<AccountDTO, Account>();
        CreateMap<Transactions, TransactionDTO>();
        CreateMap<TransactionDTO, Transactions>();
        CreateMap<Deposits, DepositDTO>();
        CreateMap<DepositDTO, Deposits>();
    }
}