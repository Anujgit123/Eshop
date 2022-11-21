using Ecommerce.Domain.Common;
using System;

namespace Ecommerce.Domain.Entities
{
    public class ProductReview : BaseEntity
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime DateCommented { get; set; }
        public string Reply { get; set; }
        public bool IsActive { get; set; }
        public string RepliedBy { get; set; }
        public DateTime DateReplied { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
