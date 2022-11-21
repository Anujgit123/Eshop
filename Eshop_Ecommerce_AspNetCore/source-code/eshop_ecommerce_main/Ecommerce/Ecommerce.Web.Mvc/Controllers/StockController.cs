using Ecommerce.Application.Handlers.Inventory.Commands;
using Ecommerce.Application.Handlers.Inventory.Queries;
using Ecommerce.Domain.Identity.Permissions;
using Ecommerce.Web.Mvc.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Controllers
{
    [AllowAnonymous]
    public class StockController : Controller
    {
        private readonly IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EntryHistory(int? page, int length, string searchValue = "", string sortColumn = "Id", string sortOrder = "Desc")
        {
            var result = await _mediator.Send(new GetProductStockHistoryWithPagingQuery { page = page, length = length, searchValue = searchValue, sortColumn = sortColumn, sortOrder = sortOrder });
            ViewData["sortOrder"] = sortOrder == "Asc" ? "Desc" : "Asc";

            return View(result);
        }

        [Authorize(Permissions.Permissions_Configuration_Stock)]
        public IActionResult LowStockProduct()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Permissions.Permissions_Configuration_Stock)]
        public async Task<IActionResult> RenderLowStockProduct()
        {
            var paging = new PageRequest().PostPageResponse(Request);
            var result = await _mediator.Send(new GetLowStockProductWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });
            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }


        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var res = await _mediator.Send(new GetProductStockByIdQuery { ProductId = (int)id });
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddStock(AddProductStockCommand addProductStock)
        {
            var res = await _mediator.Send(addProductStock);
            if (res.Succeeded) return Json(res.Data);
            return Json("false");

        }

        [HttpPost]
        public async Task<IActionResult> RemoveStock(RemoveProductStockCommand removeProductStock)
        {
            var res = await _mediator.Send(removeProductStock);
            if (res.Succeeded) return Json(res.Data);
            return Json("false");

        }

    }
}
