using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities
{
    public class Variant : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductId { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
