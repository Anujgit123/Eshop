using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Identity.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class ProfileMappingProfile : Profile
    {
        public ProfileMappingProfile()
        {
            CreateMap<ApplicationUser, EditProfileDto>().ReverseMap();
        }
    }
}
