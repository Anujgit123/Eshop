using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.OrderStatusValues.Commands;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class OrderStatusValueMappingProfile : Profile
    {
        public OrderStatusValueMappingProfile()
        {
            CreateMap<CreateOrderStatusValueCommand, OrderStatusValue>();
            CreateMap<UpdateOrderStatusValueCommand, OrderStatusValue>();
            CreateMap<UpdateOrderStatusValueCommand, OrderStatusValueDto>().ReverseMap();
            CreateMap<OrderStatusValueDto, OrderStatusValue>().ReverseMap();
        }
    }
}
