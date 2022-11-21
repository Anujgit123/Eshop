namespace Ecommerce.Application.Dto
{
    public class CustomerDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public string Gender { get; set; }
    }
}
