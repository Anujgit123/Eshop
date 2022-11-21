using Ecommerce.Application.Handlers.Categories.Queries;
using Ecommerce.Web.Mvc.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Views.Shared.Components.CategoryBreadcumb
{
    public class CategoryBreadCumbViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;
        public CategoryBreadCumbViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(BreadCumbOptionsVM option)
        {
            var result = await _mediator.Send(new GetAllParentCategoryBySlugQuery { Slug = option.Slug });

            var bc = new List<BreadCumbVM>();
            while (result != null)
            {
                bc.Add(new BreadCumbVM { Name = result.Name, Slug = result.Slug });
                result = result.ParentCategory;
            }
            bc.Reverse();
            var r = bc;
            ViewBag.CategoryBreadcumb = bc;
            ViewBag.Lastnolink = option.IsLastLink;
            return View();
        }
    }
}
