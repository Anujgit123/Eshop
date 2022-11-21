﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Views.Shared.Components.SearchSection
{
    public class SearchSectionViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string action)
        {
            ViewBag.SearchSectionAction = action;
            return View();
        }
    }
}
