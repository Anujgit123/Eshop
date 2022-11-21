namespace Ecommerce.Application.Dto
{
    public class ShippingAreaDto
    {
        public int Id { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public decimal DeliveryCharge { get; set; }
        public bool IsActive { get; set; }
    }
}
