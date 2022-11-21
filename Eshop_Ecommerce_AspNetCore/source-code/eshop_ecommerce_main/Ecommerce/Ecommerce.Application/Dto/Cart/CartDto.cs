namespace Ecommerce.Application.Dto
{
    public class CartDto
    {
        public int ProductId { get; set; }
        public int VariableId { get; set; }
        public string Title { get; set; }
        public string Sku { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
