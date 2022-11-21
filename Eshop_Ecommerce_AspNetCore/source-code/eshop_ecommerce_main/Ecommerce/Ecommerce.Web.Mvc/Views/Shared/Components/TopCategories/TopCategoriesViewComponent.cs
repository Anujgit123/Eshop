using Ecommerce.Application.Handlers.RenderItems.Queries;
using Ecommerce.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Views.Shared.Components.TopCategories
{
    public class TopCategoriesViewComponent : ViewComponent
    {
        private readonly IKeyAccessor _keyAccessor;
        private readonly IMediator _mediator;

        public TopCategoriesViewComponent(IKeyAccessor keyAccessor, IMediator mediator)
        {
            _keyAccessor = keyAccessor;
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var topCategories = await _mediator.Send(new GetTopCategoriesQuery());
            return View(topCategories);
        }
    }
}
