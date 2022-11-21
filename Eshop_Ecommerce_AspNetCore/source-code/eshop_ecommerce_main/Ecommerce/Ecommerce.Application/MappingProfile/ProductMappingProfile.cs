using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.Products.Commands;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<ProductForEditDto, Product>();
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
