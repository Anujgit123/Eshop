using Ecommerce.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string cookieValueFromReq = Request.Cookies["shop-cart"];
            if (cookieValueFromReq != null)
            {
                var cart = JsonSerializer.Deserialize<List<CartDto>>(cookieValueFromReq);
                ViewBag.Cart = cart;
            }
            return View();
        }
    }
}
