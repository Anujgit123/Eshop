using Ecommerce.Application.Handlers.Categories.Queries;
using Ecommerce.Application.Handlers.Shop.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace ECom_ProductVariable.Views.Shared.Components.ProductQuickView
{
    public class ProductQuickViewViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;
        public ProductQuickViewViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(int ProductId)
        {
            var getProductDetails = await _mediator.Send(new GetProductPreviewQuery { Id = ProductId });

            ViewData["SizeId"] = new SelectList(getProductDetails.AvailableSizes, "Id", "Name");
            ViewData["ColorId"] = new SelectList(getProductDetails.AvailableColors, "Id", "Name");
            var getCategory = await _mediator.Send(new GetCategoryByProductQuery { Id = ProductId });
            ViewBag.SelectedCategorySlug = getCategory.Slug;

            ViewBag.ProductId = ProductId;
            return View(getProductDetails.ProductDetails);
        }
    }
}
