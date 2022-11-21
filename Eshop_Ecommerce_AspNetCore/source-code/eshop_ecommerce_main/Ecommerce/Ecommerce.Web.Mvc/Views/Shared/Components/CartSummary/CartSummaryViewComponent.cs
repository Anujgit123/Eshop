using Ecommerce.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Views.Shared.Components.CartSummary
{
    public class CartSummaryViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string cookieValueFromReq = Request.Cookies["shop-cart"];

            List<CartDto> cart = new List<CartDto>();
            if (cookieValueFromReq != null)
            {
                cart = JsonSerializer.Deserialize<List<CartDto>>(cookieValueFromReq);
            }

            return View(cart);
        }
    }
}
