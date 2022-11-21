using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities
{
    public class OrderPayment : BaseEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public Order Order { get; set; }
    }
}
