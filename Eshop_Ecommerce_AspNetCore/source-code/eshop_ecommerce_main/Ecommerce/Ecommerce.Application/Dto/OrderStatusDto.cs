using Ecommerce.Domain.Entities;
using System;

namespace Ecommerce.Application.Dto
{
    public class OrderStatusDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int? OrderStatusValueId { get; set; }
        public OrderStatusValue OrderStatusValue { get; set; }
        public string Description { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
