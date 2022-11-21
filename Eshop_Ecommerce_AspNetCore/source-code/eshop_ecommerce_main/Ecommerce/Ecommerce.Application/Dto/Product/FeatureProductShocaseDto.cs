namespace Ecommerce.Application.Dto
{
    public class FeatureProductShocaseDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int CategoryId { get; set; }
        public string ProductImagePreview { get; set; }
        public FeatureProductShocaseVarientDto FeatureVarient { get; set; }
        public int Order { get; set; }
    }

    public class FeatureProductShocaseVarientDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
    }
}
