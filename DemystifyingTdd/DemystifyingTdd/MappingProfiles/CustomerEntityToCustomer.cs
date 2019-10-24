using AutoMapper;
using DemystifyingTdd.Api.Data;
using DemystifyingTdd.Api.Models;

namespace DemystifyingTdd.Api.MappingProfiles
{
    public class CustomerEntityToCustomer : Profile
    {
        public CustomerEntityToCustomer()
        {
            CreateMap<CustomerEntity, Customer>();
        }
    }
}
