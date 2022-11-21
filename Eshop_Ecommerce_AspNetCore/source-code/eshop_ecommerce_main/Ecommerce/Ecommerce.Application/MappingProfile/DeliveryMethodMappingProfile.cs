using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.DeliveryMethods.Commands;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class DeliveryMethodMappingProfile : Profile
    {
        public DeliveryMethodMappingProfile()
        {
            CreateMap<CreateDeliveryMethodCommand, DeliveryMethod>();
            CreateMap<UpdateDeliveryMethodCommand, DeliveryMethod>();
            CreateMap<UpdateDeliveryMethodCommand, DeliveryMethodDto>().ReverseMap();
            CreateMap<DeliveryMethodDto, DeliveryMethod>().ReverseMap();
        }
    }
}
