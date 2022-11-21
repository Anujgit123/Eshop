using Ecommerce.Domain.Common;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }

        public string ApplicationUserId { get; set; }
        public List<ProductReview> ProductReviews { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
    }
}
