using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class StockMappingProfile : Profile
    {
        public StockMappingProfile()
        {
            CreateMap<StockDto, Stock>().ReverseMap();
        }
    }
}
