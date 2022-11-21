using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Dto
{
    public class CheckoutDto
    {
        public int DeliveryMethod { get; set; }
        public string PaymentMethod { get; set; }
        [Required]
        //public int ShippingAreaId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        //public string CustomerCity { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerComment { get; set; }
        public StripeModel StripeModel { get; set; }
        public PaypalModel PaypalModel { get; set; }
        public bool WillSaveInfo { get; set; }
    }

    public class StripeModel
    {
        public string Token { get; set; }
        public string NameOnCard { get; set; }
    }

    public class PaypalModel
    {
        public string TransactionID { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
    }
}
