using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Identity.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class CustomerAccountProfile : Profile
    {
        public CustomerAccountProfile()
        {
            CreateMap<ApplicationUser, CustomerRegisterDto>().ReverseMap();
        }
    }
}
