using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace SUBoilerplate.Application.MappingProfile
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<ApplicationUser, AddEditUserDto>().ReverseMap();
            CreateMap<IdentityRole, ManageRoleDto>();
        }
    }
}
