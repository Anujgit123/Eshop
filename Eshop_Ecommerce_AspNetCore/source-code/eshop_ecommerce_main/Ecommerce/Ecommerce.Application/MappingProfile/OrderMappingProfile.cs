using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderDto, Order>().ReverseMap();
            //CreateMap<UpdateOrderShippingInfoDto, Order>().ReverseMap();
        }
    }
}
