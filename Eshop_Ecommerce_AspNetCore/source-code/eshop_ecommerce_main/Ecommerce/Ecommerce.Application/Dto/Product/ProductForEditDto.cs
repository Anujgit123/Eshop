using System.Collections.Generic;

namespace Ecommerce.Application.Dto
{
    public class ProductForEditDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string VariableTheme { get; set; }
        public int CategoryId { get; set; }
        public string ProductImage { get; set; }
        public string ProductImagePreview { get; set; }
        public List<ProductVarientForEditDto> ProductVarient { get; set; }
    }

    public class ProductVarientForEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductId { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public string VarientImageId { get; set; }
        public string VarientImagePreview { get; set; }
        public string Sku { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
