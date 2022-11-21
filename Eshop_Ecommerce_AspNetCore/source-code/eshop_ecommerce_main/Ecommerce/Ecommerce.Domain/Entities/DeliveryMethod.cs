using Ecommerce.Domain.Common;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities
{
    public class DeliveryMethod : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<Order> Orders { get; set; }
    }
}
