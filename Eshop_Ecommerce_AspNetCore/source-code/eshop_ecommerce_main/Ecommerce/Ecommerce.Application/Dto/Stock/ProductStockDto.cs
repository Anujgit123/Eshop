using System.Collections.Generic;

namespace Ecommerce.Application.Dto
{
    public class ProductStockDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string ProductImagePreview { get; set; }
        public List<ProductVarientStockDto> ProductVarient { get; set; }
    }

    public class ProductVarientStockDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductId { get; set; }
        public string VarientImagePreview { get; set; }
        public string Sku { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
