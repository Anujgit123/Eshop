using Ecommerce.Domain.Common;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string VariableTheme { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductReview> ProductReviews { get; set; }
    }
}
