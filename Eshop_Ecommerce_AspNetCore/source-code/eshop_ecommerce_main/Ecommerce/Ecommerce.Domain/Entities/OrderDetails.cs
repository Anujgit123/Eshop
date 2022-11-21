using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities
{
    public class OrderDetails : BaseEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductVariantId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public Order Order { get; set; }
    }
}
