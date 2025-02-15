
using AutoMapper;
using Contract;
using CustomersRoleUpdater.Application.Models;

namespace CustomersRoleUpdater.Application.Mappings;

public class CustomersMapperProfile : Profile
{
    public CustomersMapperProfile()
    {
        CreateMap<Customer, ListCustomerId>().ReverseMap();
    }
}
