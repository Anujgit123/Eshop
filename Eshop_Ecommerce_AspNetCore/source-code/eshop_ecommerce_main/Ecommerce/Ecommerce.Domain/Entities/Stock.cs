using Ecommerce.Domain.Common;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities
{
    public class Stock : BaseEntity
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public Variant Variant { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public StockInputType StockInputType { get; set; }

    }
}
