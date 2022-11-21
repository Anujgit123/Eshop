using AutoMapper;
using Ecommerce.Application.Dto;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.MappingProfile
{
    class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<IdentityRole, RoleDto>();
            CreateMap<IdentityRole, AddEditRoleDto>().ReverseMap();
        }
    }
}
