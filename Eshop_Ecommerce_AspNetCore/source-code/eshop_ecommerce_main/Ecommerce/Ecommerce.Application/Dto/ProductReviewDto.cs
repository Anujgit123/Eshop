using System;

namespace Ecommerce.Application.Dto
{
    public class ProductReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateCommented { get; set; }
        public string Reply { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public bool IsActive { get; set; }
    }
}
