using System.Collections.Generic;

namespace Ecommerce.Application.Dto
{
    public class ShopDetailsDto
    {
        public ProductDetailsDto ProductDetails { get; set; }
        public List<ColorDto> AvailableColors { get; set; }
        public List<SizeDto> AvailableSizes { get; set; }
    }
}
