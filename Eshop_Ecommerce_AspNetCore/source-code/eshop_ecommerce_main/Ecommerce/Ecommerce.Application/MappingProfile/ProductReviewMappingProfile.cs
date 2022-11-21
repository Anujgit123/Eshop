using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.ProductReviews.Commands;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.MappingProfile
{
    public class ProductReviewMappingProfile : Profile
    {
        public ProductReviewMappingProfile()
        {
            CreateMap<CreateProductReviewByCustomerCommand, ProductReview>();
            CreateMap<CreateReplyForCustomerReviewCommand, ProductReview>();
            CreateMap<ProductReviewDto, ProductReview>().ReverseMap();
        }
    }
}
