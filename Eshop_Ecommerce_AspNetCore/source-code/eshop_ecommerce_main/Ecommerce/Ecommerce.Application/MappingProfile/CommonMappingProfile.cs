using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.ContactQueries.Commands;
using Ecommerce.Application.Handlers.Customers.Commands;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<UpdateCustomerCommand, CustomerDto>().ReverseMap();
            CreateMap<UpdateCustomerCommand, Customer>();

            CreateMap<ContactQueryDto, ContactQuery>().ReverseMap();
            CreateMap<CreateContactQueryCommand, ContactQueryDto>().ReverseMap();
            CreateMap<CreateContactQueryCommand, ContactQuery>().ReverseMap();
        }
    }
}
