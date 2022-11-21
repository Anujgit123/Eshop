using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Constants;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Dashboard.Queries
{
    public class GetDashboardSummaryQuery : IRequest<DashboardDto>
    {
    }
    public class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        private readonly IKeyAccessor _keyAccessor;
        public GetDashboardSummaryQueryHandler(IDataContext db, IMapper mapper, IKeyAccessor keyAccessor)
        {
            _db = db;
            _mapper = mapper;
            _keyAccessor = keyAccessor;
        }

        public async Task<DashboardDto> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
            StockConfiguration conStock = JsonSerializer.Deserialize<StockConfiguration>(_keyAccessor.GetSection(AppConfigurationType.StockConfiguration));
            var pendingOrderStatus = await _db.OrderStatusValues.Where(o => o.StatusValue == DefaultOrderStatusValue.Pending().StatusValue).FirstOrDefaultAsync();
            var pendingCount = _db.Orders
                .Include(o => o.OrderStatus)
                .ThenInclude(o => o.OrderStatusValue)
                .Where(o => o.OrderStatus.OrderByDescending(o => o.Id).Select(o => o.OrderStatusValueId).FirstOrDefault() == pendingOrderStatus.Id).Count();

            List<Order> todaysSales = _db.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.OrderStatus)
                .ThenInclude(o => o.OrderStatusValue)
            .Where(o => o.CreatedDate >= DateTime.Today.AddDays(0) && o.CreatedDate < DateTime.Today.AddDays(1)).ToList();

            //List<Order> thisWeeksSales = _db.Orders
            //    .Include(o => o.OrderDetails)
            //    .Include(o => o.OrderStatus)
            //    .ThenInclude(o => o.OrderStatusValue)
            //.Where(o => o.CreatedDate >= DateTime.Today.AddDays(-6) && o.CreatedDate < DateTime.Today.AddDays(1)).ToList();

            //List<WeeklySalesCount> weeklyItemSalesCount = new()
            //{
            //    new WeeklySalesCount { OrderDate = DateTime.Today.AddDays(0).Date, NameOfDay = DateTime.Today.AddDays(0).DayOfWeek.ToString(), Count = thisWeeksSales.Where(o => o.CreatedDate >= DateTime.Today.AddDays(0) && o.CreatedDate < DateTime.Today.AddDays(1)).Select(o => o.OrderDetails.Select(o => o.Qty).Sum()).Sum() },
            //    new WeeklySalesCount { OrderDate = DateTime.Today.AddDays(-1).Date, NameOfDay = DateTime.Today.AddDays(-1).DayOfWeek.ToString(), Count = thisWeeksSales.Where(o => o.CreatedDate >= DateTime.Today.AddDays(-1) && o.CreatedDate < DateTime.Today.AddDays(0)).Select(o => o.OrderDetails.Select(o => o.Qty).Sum()).Sum() },
            //    new WeeklySalesCount { OrderDate = DateTime.Today.AddDays(-2).Date, NameOfDay = DateTime.Today.AddDays(-2).DayOfWeek.ToString(), Count = thisWeeksSales.Where(o => o.CreatedDate >= DateTime.Today.AddDays(-2) && o.CreatedDate < DateTime.Today.AddDays(-1)).Select(o => o.OrderDetails.Select(o => o.Qty).Sum()).Sum() },
            //    new WeeklySalesCount { OrderDate = DateTime.Today.AddDays(-3).Date, NameOfDay = DateTime.Today.AddDays(-3).DayOfWeek.ToString(), Count = thisWeeksSales.Where(o => o.CreatedDate >= DateTime.Today.AddDays(-3) && o.CreatedDate < DateTime.Today.AddDays(-2)).Select(o => o.OrderDetails.Select(o => o.Qty).Sum()).Sum() },
            //    new WeeklySalesCount { OrderDate = DateTime.Today.AddDays(-4).Date, NameOfDay = DateTime.Today.AddDays(-4).DayOfWeek.ToString(), Count = thisWeeksSales.Where(o => o.CreatedDate >= DateTime.Today.AddDays(-4) && o.CreatedDate < DateTime.Today.AddDays(-3)).Select(o => o.OrderDetails.Select(o => o.Qty).Sum()).Sum() },
            //    new WeeklySalesCount { OrderDate = DateTime.Today.AddDays(-5).Date,NameOfDay = DateTime.Today.AddDays(-5).DayOfWeek.ToString(), Count = thisWeeksSales.Where(o => o.CreatedDate >= DateTime.Today.AddDays(-5) && o.CreatedDate < DateTime.Today.AddDays(-4)).Select(o => o.OrderDetails.Select(o => o.Qty).Sum()).Sum() },
            //    new WeeklySalesCount { OrderDate = DateTime.Today.AddDays(-6).Date,NameOfDay = DateTime.Today.AddDays(-6).DayOfWeek.ToString(), Count = thisWeeksSales.Where(o => o.CreatedDate >= DateTime.Today.AddDays(-6) && o.CreatedDate < DateTime.Today.AddDays(-5)).Select(o => o.OrderDetails.Select(o => o.Qty).Sum()).Sum() },

            //};

            //var vdWeeklySales = _db.Orders.ExecuteSqlRaw<WeeklySalesCount>("Proc_GetVDWeeklySales").ToList();


            //var thisWeek = DateTime.Now.AddDays(-6).Range(DateTime.Now).Select(o => o.Date).ToList();

            //var thisWeekSalesCount = (from o in _db.Orders
            //                          join op in thisWeek on new { Id = (DateTime?)Convert.ToDateTime(o.CreatedDate.Value.Date) } equals new { Id = (DateTime?)Convert.ToDateTime(op.Date) } into op_join
            //                          from op in op_join.DefaultIfEmpty()
            //                          select new
            //                          {
            //                              DayOfWeek = op.DayOfWeek,
            //                              OrderAmount = o.OrderAmount,
            //                          }).ToList();

            //var thisWeekSalesCount = (from o in _db.Orders
            //                          join op in _db.OrderPayments on new { Id = o.Id } equals new { Id = op.OrderId } into op_join
            //                          from op in op_join.DefaultIfEmpty()
            //                          group o by new
            //                          {
            //                              o.CreatedDate
            //                          } into g
            //                          orderby
            //                            g.Key.CreatedDate
            //                          select new
            //                          {
            //                              g.Key.CreatedDate,
            //                              WeekShort = g.Key.CreatedDate,
            //                              Total = ((Int64?)g.Count(p => p.Id != null) ?? (Int64?)0),
            //                              Sales = ((System.Decimal?)g.Sum(p => p.OrderAmount + p.DeliveryCharge) ?? (System.Decimal?)0),
            //                              //SalesCollect = ((System.Decimal?)g.Sum(p => p.Amount) ?? (System.Decimal?)0)
            //                          }).ToList();

            //var catgroup = _db.Orders.Include(o => o.OrderStatus).ThenInclude(o => o.OrderStatusValue).GroupBy(c => c.CreatedDate.Value.Date).
            //      Select(g => new WeeklySalesAmount
            //      {
            //          NameOfDay = g.Key.ToString(),
            //          OrderAmount = g.Sum(s => s.OrderAmount)
            //      }).ToList();


            //try
            //{
            //    var thisWeekSalesAmount = await (from o in _db.Orders.Include(o => o.OrderStatus).ThenInclude(o => o.OrderStatusValue)
            //                                     join w in thisWeek on o.CreatedDate.Value.Date equals w.Date into wlist
            //                                     from w in wlist.DefaultIfEmpty()
            //                                     select new WeeklySalesAmount
            //                                     {
            //                                         NameOfDay = w.DayOfWeek.ToString(),
            //                                         Total = o.OrderAmount + (decimal)o.DeliveryCharge
            //                                     }).ToListAsync();
            //}
            //catch (Exception ex)
            //{

            //}

            var lowStockCount = await _db.Variants.Where(o => o.Quantity <= conStock.OutOfStockThreshold).CountAsync();


            DashboardDto dashboard = new();
            dashboard.PendingOrderCount = pendingCount;
            dashboard.LowStockItemCount = lowStockCount;
            //dashboard.TodaySalesSummary.TotalItem = todaysSales.Count() > 0 ? 5:0;

            TodaySalesSummary salesSummary = new()
            {
                TotalProduct = todaysSales.SelectMany(o => o.OrderDetails.Select(p => p.ProductVariantId)).Distinct().Count(),
                TotalItem = todaysSales.Select(o => o.OrderDetails.Select(p => p.Qty).Sum()).Sum(),
                TotalSales = todaysSales.Select(o => o.Id).Count(),
                TotalSalesAmount = todaysSales.Select(o => o.OrderAmount).Sum(),
                TotalDeliveryCharge = (decimal)todaysSales.Select(o => o.DeliveryCharge).Sum(),
            };

            CustomerInfo customerInfo = new()
            {
                TotalCustomer = _db.Customers.Count(),
                TotalCustomerToday = _db.Customers.Count(o => o.CreatedDate >= DateTime.Today.AddDays(0) && o.CreatedDate < DateTime.Today.AddDays(1))
            };

            dashboard.TodaySalesSummary = salesSummary;
            dashboard.CustomerInfo = customerInfo;

            //dashboard.TodaySalesSummary.TotalProduct = todaysSales != null ? todaysSales.Select(o => o.OrderDetails.Select(o => o.Qty)).Count() : 0;
            //dashboard.TodaySalesSummary.TotalSalesAmount = todaysSales != null ? todaysSales.Select(o => o.OrderAmount).Sum() : 0;
            //dashboard.TodaySalesSummary.TotalDeliveryCharge = todaysSales != null ? ((decimal)todaysSales.Select(o => o.DeliveryCharge).Sum()) : 0;

            return dashboard;
        }
    }
}
