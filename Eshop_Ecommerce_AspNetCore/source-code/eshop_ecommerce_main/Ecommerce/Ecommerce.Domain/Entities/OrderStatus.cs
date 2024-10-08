﻿using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities
{
    public class OrderStatus : BaseEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int? OrderStatusValueId { get; set; }
        public OrderStatusValue OrderStatusValue { get; set; }
        public string Description { get; set; }
    }
}
