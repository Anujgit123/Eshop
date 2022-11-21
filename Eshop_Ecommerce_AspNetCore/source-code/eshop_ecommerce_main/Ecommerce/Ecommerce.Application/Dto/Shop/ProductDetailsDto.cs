using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Ecommerce.Application.Dto
{
    public class ProductDetailsDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string VariableTheme { get; set; }
        public int CategoryId { get; set; }
        public string ProductImage { get; set; }
        public string ProductImagePreview { get; set; }
        public string Price { get; set; }
        public ProductDetailsVarientDto FeatureVarient { get; set; }
        public List<ProductDetailsVarientDto> Varient { get; set; }
        public List<ColorDto> AvailableColorVarient { get; set; }
        public List<SizeDto> AvailableSizesVarient { get; set; }
        public List<ProductReviewsDto> CustomerReviews { get; set; }
    }

    public class ProductDetailsVarientDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public string VarientImagePreview { get; set; }
        public string Sku { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductReviewsDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateCommented { get; set; }
        public string Reply { get; set; }
        //public string RepliedBy { get; set; }
        public DateTime DateReplied { get; set; }
        //public int ProductId { get; set; }
    }
}
