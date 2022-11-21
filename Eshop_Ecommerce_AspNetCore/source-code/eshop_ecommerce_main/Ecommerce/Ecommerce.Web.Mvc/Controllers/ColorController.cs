using AutoMapper;
using Ecommerce.Application.Handlers.Colors.Commands;
using Ecommerce.Application.Handlers.Colors.Queries;
using Ecommerce.Domain.Identity.Permissions;
using Ecommerce.Web.Mvc.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Controllers
{
    [Authorize]
    public class ColorController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ColorController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [Authorize(Permissions.Permissions_Color_View)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Permissions.Permissions_Color_View)]
        public async Task<IActionResult> RenderView()
        {
            var paging = new PageRequest().GetPageResponse(Request);
            var result = await _mediator.Send(new GetColorsWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });

            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }

        //public async Task<IActionResult> RenderColor(int? page, int length, string searchValue = "", string sortColumn = "Id", string sortOrder = "Desc")
        //{
        //    var result = await _mediator.Send(new GetColorsWithPagingQuery { page = page, length = length, searchValue = searchValue, sortColumn = sortColumn, sortOrder = sortOrder });

        //    PartialViewResult partialView = new PartialViewResult()
        //    {
        //        ViewName = "_RenderColor",
        //        ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        //        {
        //            Model = result,
        //        }
        //    };

        //    return partialView;
        //}

        [Authorize(Permissions.Permissions_Color_Create)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Permissions.Permissions_Color_Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateColorCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                if (response.Succeeded) return RedirectToAction(nameof(Index));
            }

            return View(command);
        }

        [Authorize(Permissions.Permissions_Color_Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var color = await _mediator.Send(new GetColorByIdQuery { Id = id });
            if (color == null)
            {
                return NotFound();
            }

            var colorCommand = _mapper.Map<UpdateColorCommand>(color);
            return View(colorCommand);
        }

        [Authorize(Permissions.Permissions_Color_Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateColorCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        [Authorize(Permissions.Permissions_Color_Delete)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteColorCommand { Id = id });
            return Json(response);
        }
    }
}
