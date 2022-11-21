using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.Categories.Commands;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, CategoryDto>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
        }
    }
}
