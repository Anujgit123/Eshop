using Ecommerce.Application.Helpers;
using System.Collections.Generic;

namespace Ecommerce.Application.Dto
{
    public class ShopDto
    {
        public PaginatedList<ProductShocaseDto> PaginatedProductList { get; set; }
        //public List<ProductShocaseDto> ProductList { get; set; }
    }
}
