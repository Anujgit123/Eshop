using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class OrderStatusMappingProfile : Profile
    {
        public OrderStatusMappingProfile()
        {
            CreateMap<OrderStatusDto, OrderStatus>().ReverseMap();
        }
    }
}
