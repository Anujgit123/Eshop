using Ecommerce.Domain.Entities;
using System.Collections.Generic;

namespace Ecommerce.Application.Dto
{
    public class NewProductShocaseDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string VariableTheme { get; set; }
        public int CategoryId { get; set; }
        public string ProductImage { get; set; }
        public string ProductImagePreview { get; set; }
        public NewProductShocaseVarientDto FeatureVarient { get; set; }
        public IList<ColorDto> AvailableColorVarient { get; set; }
        public IList<SizeDto> AvailableSizesVarient { get; set; }
    }

    public class NewProductShocaseVarientDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public string VarientImagePreview { get; set; }
        public string Sku { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
