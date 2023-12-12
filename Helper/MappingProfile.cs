using AutoMapper;
using BillingAPI.DTO;
using BillingAPI.Models;

namespace BillingAPI.Helper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDTO>();
        CreateMap<CustomerDTO, Customer>();
        CreateMap<Account, AccountDTO>();
        CreateMap<AccountDTO, Account>();
    }
}