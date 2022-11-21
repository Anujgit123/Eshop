using Ecommerce.Application.Handlers.RenderItems.Queries;
using Ecommerce.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Views.Shared.Components.NewArrival
{
    public class NewArrivalViewComponent : ViewComponent
    {
        private readonly IKeyAccessor _keyAccessor;
        private readonly IMediator _mediator;

        public NewArrivalViewComponent(IKeyAccessor keyAccessor, IMediator mediator)
        {
            _keyAccessor = keyAccessor;
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var productShocases = await _mediator.Send(new GetNewProductQuery());

            var availableColor = productShocases.SelectMany(o => o.AvailableColorVarient).Where(o => o != null).Distinct().OrderBy(o => o.Name).ToList();
            var availableSize = productShocases.SelectMany(o => o.AvailableSizesVarient).Where(o => o != null).Distinct().ToList();

            ViewBag.AvailableColor = availableColor;
            ViewBag.AvailableSize = availableSize;

            return View(productShocases);
        }
    }
}
