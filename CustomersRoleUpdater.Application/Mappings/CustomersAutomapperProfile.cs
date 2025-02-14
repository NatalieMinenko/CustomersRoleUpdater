using AutoMapper;
using CustomersRoleUpdater.Application.Models;
using Contract;

namespace CustomersRoleUpdater.Application.Mappings
{
    public class CustomersAutomapperProfile : Profile
    {
        public CustomersAutomapperProfile() 
        {
            CreateMap<Customer, CustomerIdsModel>().ReverseMap();
        }
    }
}
