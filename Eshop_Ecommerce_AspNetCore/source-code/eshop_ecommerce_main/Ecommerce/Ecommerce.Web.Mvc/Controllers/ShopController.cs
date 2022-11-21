using Ecommerce.Application.Dto;
using Ecommerce.Application.Extension;
using Ecommerce.Application.Handlers.Categories.Queries;
using Ecommerce.Application.Handlers.Colors.Queries;
using Ecommerce.Application.Handlers.Shop.Queries;
using Ecommerce.Application.Handlers.Sizes.Queries;
using Ecommerce.Web.Mvc.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        private readonly IMediator _mediator;
        public ShopController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index(string color, string size, int? page, string sortColumn = "ProductId", string sortOrder = "Desc")
        {
            var getShopItems = await _mediator.Send(new GetShopWithPagingQuery { color = color, size = size, page = page, pageSize = 9, sortColumn = sortColumn, sortOrder = sortOrder });

            var productList = getShopItems.PaginatedProductList.Items;

            var availableColor = productList.SelectMany(o => o.AvailableColorVarient).Where(o => o != null).DistinctBy(o => o.Name).OrderBy(o => o.Name).ToList();
            var availableSize = productList.SelectMany(o => o.AvailableSizesVarient).Where(o => o != null).DistinctBy(o => o.Name).ToList();
            var availableCategory = await _mediator.Send(new GetCategoriesQuery());

            ViewBag.AvailableColor = availableColor;
            ViewBag.AvailableSize = availableSize;
            ViewBag.AvailableCategory = availableCategory.Where(o => o.ParentCategoryId == null);

            ViewBag.SelectedColor = color;
            ViewBag.SelectedColorName = color != null ? (await _mediator.Send(new GetColorByIdQuery { Id = int.Parse(color) })).Name : null;
            ViewBag.SelectedSize = size;
            ViewBag.SelectedSizeName = size != null ? (await _mediator.Send(new GetSizeByIdQuery { Id = int.Parse(size) })).Name : null;

            return View(getShopItems.PaginatedProductList);
        }

        public IEnumerable<CategoryDto> ListFlatten(CategoryDto dto)
        {
            yield return dto;
            if (dto.Children == null)
            { // or it's got .Count==0, your call
                yield break;
            }
            foreach (var child in dto.Children)
            {
                foreach (var flattenedNode in ListFlatten(child))
                {
                    yield return flattenedNode;
                }
            }
        }

        [Route("shop/{id}")]
        public async Task<IActionResult> ByCategory(string id, string color, string size, int? page, string sortColumn = "ProductId", string sortOrder = "Desc")
        {
            var selectedCategory = await _mediator.Send(new GetCategoryBySlugQuery { Slug = id });
            var selectedCategoryList = await _mediator.Send(new GetAllChildrenCategoryByIdQuery { Id = selectedCategory.Id });


            string[] slug = Array.Empty<string>();

            //var breadCumbs = new List<BreadCumbVM>();
            foreach (var result in ListFlatten(selectedCategoryList))
            {
                //breadCumbs.Add(new BreadCumbVM { Name = result.Name, Slug = result.Slug });
                slug = slug.Append(result.Slug).ToArray();
            }


            var getShopItems = await _mediator.Send(new GetShopByCategoryWithPagingQuery { Slug = slug, color = color, size = size, page = page, pageSize = 9, sortColumn = sortColumn, sortOrder = sortOrder });
            var productList = getShopItems.PaginatedProductList.Items;

            var availableColor = productList.SelectMany(o => o.AvailableColorVarient).Where(o => o != null).DistinctBy(o => o.Name).OrderBy(o => o.Name).ToList();
            var availableSize = productList.SelectMany(o => o.AvailableSizesVarient).Where(o => o != null).DistinctBy(o => o.Name).ToList();
            var availableCategory = await _mediator.Send(new GetCategoriesQuery());

            ViewBag.SelectedCategory = selectedCategory.Name;
            ViewBag.SelectedCategorySlug = id;
            ViewBag.AvailableColor = availableColor;
            ViewBag.AvailableSize = availableSize;
            ViewBag.AvailableCategory = availableCategory.ToList()[0].Children;

            ViewBag.SelectedColor = color;
            ViewBag.SelectedColorName = color != null ? (await _mediator.Send(new GetColorByIdQuery { Id = int.Parse(color) })).Name : null;
            ViewBag.SelectedSize = size;
            ViewBag.SelectedSizeName = size != null ? (await _mediator.Send(new GetSizeByIdQuery { Id = int.Parse(size) })).Name : null;

            return View(getShopItems.PaginatedProductList);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var getProductDetails = await _mediator.Send(new GetProductPreviewQuery { Id = id });

            ViewData["SizeId"] = new SelectList(getProductDetails.AvailableSizes, "Id", "Name");
            ViewData["ColorId"] = new SelectList(getProductDetails.AvailableColors, "Id", "Name");
            var getCategory = await _mediator.Send(new GetCategoryByProductQuery { Id = id });

            ViewBag.SelectedCategorySlug = getCategory.Slug;
            ViewBag.ProductId = id;
            var d = getProductDetails.ProductDetails;
            return View(getProductDetails.ProductDetails);
        }

        [Route("filtersizebycolor/{id}")]
        public async Task<IActionResult> SizeByColorFilter(int id, string color)
        {
            var result = await _mediator.Send(new GetProductSizesByColorQuery { Id = id, Color = color });
            return Json(result);
        }

        [Route("filterdetails/{id}")]
        public async Task<IActionResult> ProductDetailsFilter(int id, string color, string size)
        {
            var result = await _mediator.Send(new GetProductDetailsFilterResultQuery { Id = id, Color = color, Size = size });
            return Json(result);
        }

        [Route("quickview/{id}")]
        public IActionResult ProductQuickView(int id)
        {
            return ViewComponent("ProductQuickView", new { ProductId = id });
        }

    }
}
