using Ecommerce.Domain.Common;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public string UserId { get; set; }
        public decimal OrderAmount { get; set; }
        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        //public int ShippingAreaId { get; set; }
        //public ShippingArea ShippingArea { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        //public string CustomerCity { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerComment { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public List<OrderStatus> OrderStatus { get; set; }
        public OrderPayment OrderPayments { get; set; }
    }
}
