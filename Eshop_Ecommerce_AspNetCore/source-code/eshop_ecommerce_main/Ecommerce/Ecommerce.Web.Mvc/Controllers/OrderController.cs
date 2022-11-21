using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.Customers.Queries;
using Ecommerce.Application.Handlers.DeliveryMethods.Queries;
using Ecommerce.Application.Handlers.Orders.Commands;
using Ecommerce.Application.Handlers.Orders.Queries;
using Ecommerce.Application.Handlers.OrderStatusValues.Queries;
using Ecommerce.Domain.Identity.Permissions;
using Ecommerce.Web.Mvc.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [Authorize(Permissions.Permissions_Order_View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RenderView()
        {
            var paging = new PageRequest().PostPageResponse(Request);
            var result = await _mediator.Send(new GetOrdersWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });

            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }

        [HttpGet]
        [Route("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var customerInfo = await _mediator.Send(new GetCustomerInfoByLoginUserQuery());
            ViewBag.CustomerInfo = customerInfo;
            string cookieValueFromReq = Request.Cookies["shop-cart"];
            if (cookieValueFromReq != null && JsonSerializer.Deserialize<List<CartDto>>(cookieValueFromReq)!.Count != 0)
            {
                var dm = await _mediator.Send(new GetDeliveryMethodsQuery());

                ViewData["DeliveryMethod"] = dm.Where(o => o.IsActive);
                return View();

            }
            TempData["notification"] = "<script>swal(`" + "Your Cart in Empty!" + "`, `" + "Please Add Some Items." + "`,`" + "warning" + "`)" + "</script>";
            return RedirectToAction("Index", "Shop");
        }

        [HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> Checkout(CheckoutDto checkoutDto)
        {
            var status = false;
            if (!ModelState.IsValid) return Json(new JsonResponse { Success = status });

            var response = await _mediator.Send(new CreateOrderCheckoutCommand { Checkout = checkoutDto });
            if (response.Succeeded) status = true;

            TempData["OrderId"] = Convert.ToInt32(response.Message);
            var jresponse = new JsonResponse { Success = status, Item = response.Message, Message = response.Message };
            return Json(jresponse);

        }

        public IActionResult Invoice()
        {
            return View();
        }

        [HttpGet]
        [Route("order-completed")]
        public async Task<IActionResult> OrderCompleted()
        {
            var orderId = Convert.ToInt32(TempData["OrderId"]);

            var order = await _mediator.Send(new GetOrderInvoiceByOrderIdQuery { Id = orderId });
            ViewBag.OrderInvoice = order;
            //TempData["notification"] = "<script>swal(`" + "Order Completed!" + "`, `" + "You can download your invoice." + "`,`" + "success" + "`)" + "</script>";
            return View();


            //return ViewComponent("OrderCompletedView", new { id = id });
        }

        [HttpGet]
        [Authorize(Permissions.Permissions_Order_View)]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null) return NotFound();

            var orderDetails = await _mediator.Send(new GetOrderDetailsByIdQuery { OrderId = id });

            ViewData["OrderStatusId"] = new SelectList(await _mediator.Send(new GetOrderStatusValuesQuery()), "Id", "StatusValue", orderDetails.CurrentOrderStatus.OrderStatusValue.Id);
            return View(orderDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permissions_Order_View)]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusDto updateOrderStatus)
        {
            if (updateOrderStatus?.OrderId == null) return NotFound();

            ViewData["OrderStatusId"] = new SelectList(await _mediator.Send(new GetOrderStatusValuesQuery()), "Id", "StatusValue", updateOrderStatus.NewOrderStatus);

            var respose = await _mediator.Send(new UpdateOrderStatusCommand { UpdateOrderStatus = updateOrderStatus });
            if (respose.Succeeded) return Json("success");
            return Json("failed");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permissions_Order_View)]
        public async Task<IActionResult> UpdateShippingInfo(UpdateOrderShippingInfoDto updateOrderShippingInfo)
        {
            if (updateOrderShippingInfo?.OrderId == null) return NotFound();


            var respose = await _mediator.Send(new UpdateOrderShippingInfoCommand { UpdateOrderShipping = updateOrderShippingInfo });
            if (respose.Succeeded) return Json("success");
            return Json("failed");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permissions_Order_View)]
        public async Task<IActionResult> AddCODPayment(AddCODPaymentDto addCodPayment)
        {
            if (addCodPayment?.OrderId == null) return NotFound();


            var respose = await _mediator.Send(new CreateCODPaymentCommand { AddCODPayment = addCodPayment });
            if (respose.Succeeded) return Json("success");
            return Json("failed");
        }





        [Authorize(Permissions.Permissions_Order_View)]
        public IActionResult Pending()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PendingRenderView()
        {
            var paging = new PageRequest().PostPageResponse(Request);
            var result = await _mediator.Send(new GetPendingOrdersWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });

            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }
        [Authorize(Permissions.Permissions_Order_View)]
        public IActionResult Cancelled()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CancelledRenderView()
        {
            var paging = new PageRequest().PostPageResponse(Request);
            var result = await _mediator.Send(new GetCancelledOrdersWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });

            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }

        [Authorize(Permissions.Permissions_Order_View)]
        public IActionResult Delivered()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeliveredRenderView()
        {
            var paging = new PageRequest().PostPageResponse(Request);
            var result = await _mediator.Send(new GetDeliveredOrdersWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });

            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }

        [Authorize(Permissions.Permissions_Order_View)]
        public IActionResult Completed()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompletedRenderView()
        {
            var paging = new PageRequest().PostPageResponse(Request);
            var result = await _mediator.Send(new GetCompletedOrdersWithPagingQuery { page = paging.PageIndex, length = paging.Length, searchValue = paging.SearchValue, sortColumn = paging.SortColumnName, sortOrder = paging.SortOrder });

            var jsonData = new { data = result.Items, draw = paging.Draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount };
            return Json(jsonData);
        }





    }
}
