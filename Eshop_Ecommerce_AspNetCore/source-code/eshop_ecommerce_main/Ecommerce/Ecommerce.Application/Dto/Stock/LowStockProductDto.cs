namespace Ecommerce.Application.Dto.Stock
{
    public class LowStockProductDto
    {
        public int ProductId { get; set; }
        public int VariantId { get; set; }
        public string Category { get; set; }
        public string ProductTitle { get; set; }
        public string VariantTitle { get; set; }
        public int Quantity { get; set; }
        public int TotalVariant { get; set; }
        public int LowStockVariantCount { get; set; }
    }
}