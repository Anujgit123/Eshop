using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class GalleryMappingProfile : Profile
    {
        public GalleryMappingProfile()
        {
            CreateMap<GalleryDto, Gallery>().ReverseMap();
        }
    }
}
