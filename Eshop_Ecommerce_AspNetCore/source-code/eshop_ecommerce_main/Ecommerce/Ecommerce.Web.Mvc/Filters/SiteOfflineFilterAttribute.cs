using Ecommerce.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Threading.Tasks;
using System;
using System.Text.Json;
using Ecommerce.Domain.Common;
using PayPal.Api;
using Ecommerce.Domain.Models;

namespace Ecommerce.Web.Mvc.Filters
{
    public class SiteOfflineFilterAttribute : ActionFilterAttribute
    {
        private readonly IKeyAccessor _keyAccessor;
        private readonly IMediator _mediator;

        public SiteOfflineFilterAttribute(IKeyAccessor keyAccessor, IMediator mediator)
        {
            _keyAccessor = keyAccessor;
            _mediator = mediator;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            AdvancedConfiguration conAdv = JsonSerializer.Deserialize<AdvancedConfiguration>(_keyAccessor.GetSection("AdvancedConfiguration")) ?? new AdvancedConfiguration();

            var requestMethod = context.HttpContext.Request.Method;
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            var host = request.Host;

            if (conAdv.IsComingSoonEnabled)
            {
                response.Redirect("/comingsoon", true);
            }
            var d = await next();
        }
    }
}
