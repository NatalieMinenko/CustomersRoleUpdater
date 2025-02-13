using AutoMapper;
using CustomersRoleUpdater.Application.Models;
using RoleRenewalContract;

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
