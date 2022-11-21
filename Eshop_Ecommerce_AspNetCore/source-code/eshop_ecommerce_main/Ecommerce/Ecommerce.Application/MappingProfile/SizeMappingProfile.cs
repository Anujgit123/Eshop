using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.Sizes.Commands;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class SizeMappingProfile : Profile
    {
        public SizeMappingProfile()
        {
            CreateMap<CreateSizeCommand, Size>();
            CreateMap<UpdateSizeCommand, Size>();
            CreateMap<UpdateSizeCommand, SizeDto>().ReverseMap();
            CreateMap<SizeDto, Size>().ReverseMap();
        }
    }
}
