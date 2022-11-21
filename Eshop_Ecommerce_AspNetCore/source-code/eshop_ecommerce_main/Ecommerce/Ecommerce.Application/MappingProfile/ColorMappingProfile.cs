using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.Colors.Commands;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class ColorMappingProfile : Profile
    {
        public ColorMappingProfile()
        {
            CreateMap<CreateColorCommand, Color>();
            CreateMap<UpdateColorCommand, Color>();
            CreateMap<UpdateColorCommand, ColorDto>().ReverseMap();
            CreateMap<ColorDto, Color>().ReverseMap();
        }
    }
}
