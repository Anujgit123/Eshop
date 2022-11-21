using AspNetCore.Reporting;
using AutoMapper;
using Ecommerce.Application.Handlers.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Controllers
{
    public class ReportController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public ReportController(IMediator mediator, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("order-invoice/{id}")]
        public async Task<IActionResult> OrderInvoice(int id = 7)
        {

            try
            {
                var currentProject = Assembly.GetCallingAssembly().GetName().Name;
                string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace($"{currentProject}.dll", string.Empty);
                string rdlcFilePath = string.Format("{0}report\\rdlc\\{1}.rdlc", fileDirPath, "Invoice");
                var path = $"{_webHostEnvironment.WebRootPath}\\report\\rdlc\\Invoice.rdlc";

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding.GetEncoding("utf-8");
                LocalReport report = new LocalReport(path);

                var order = await _mediator.Send(new GetRptOrderInvoiceByOrderIdQuery { Id = id });


                report.AddDataSource("dsReport", order.OrderItems);
                report.AddDataSource("dsReportCustomerInfo", order.CustomerInfo);
                report.AddDataSource("dsReportOrderInfo", order.OrderSummary);
                report.AddDataSource("dsReportOrderAmount", order.OrderAmount);
                report.AddDataSource("dsReportPageInfos", order.ReportPageInfos);
                var result = report.Execute(RenderType.Pdf, 1);
                return File(result.MainStream, System.Net.Mime.MediaTypeNames.Application.Octet, order.InvoiceNo + ".pdf");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
