using System;

namespace Ecommerce.Application.Dto
{
    public class CustomerOrderDto
    {
        public int OrderId { get; set; }
        public string InvoiceNo { get; set; }
        public decimal OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
