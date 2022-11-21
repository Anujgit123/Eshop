using Ecommerce.Domain.Common;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities
{
    public class OrderStatusValue : BaseEntity
    {
        public int Id { get; set; }
        public string StatusValue { get; set; }
        public string Description { get; set; }
        public List<OrderStatus> OrderStatus { get; set; }
    }
}
