using System;

namespace Ecommerce.Application.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public string UserId { get; set; }
        public decimal OrderAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string ShippingAreaName { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public string PaymentMethod { get; set; }
        public string CurrentOrderStatus { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
