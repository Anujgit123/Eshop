﻿using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Orders.Queries
{
    public class GetOrdersWithPagingQuery : IRequest<PaginatedList<OrderDto>>
    {
        public int? page { get; set; }
        public int length { get; set; }
        public string searchValue { get; set; } = "";
        public string sortColumn { get; set; } = "Id";
        public string sortOrder { get; set; } = "Desc";
    }
    public class GetOrdersWithPagingQueryHandler : IRequestHandler<GetOrdersWithPagingQuery, PaginatedList<OrderDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetOrdersWithPagingQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaginatedList<OrderDto>> Handle(GetOrdersWithPagingQuery request, CancellationToken cancellationToken)
        {
            var orders = _db.Orders.AsNoTracking().Include(o => o.OrderDetails)
                .Include(o => o.OrderStatus)
                .ThenInclude(o => o.OrderStatusValue)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    InvoiceNo = o.InvoiceNo,
                    UserId = o.UserId,
                    CustomerName = o.CustomerName,
                    CustomerAddress = o.CustomerAddress,
                    OrderAmount = o.OrderAmount,
                    PaymentStatus = o.PaymentStatus,
                    DeliveryCharge = o.DeliveryCharge,
                    PaymentMethod = o.PaymentMethod,
                    CurrentOrderStatus = o.OrderStatus.OrderByDescending(o => o.Id).Select(o => o.OrderStatusValue).AsQueryable().FirstOrDefault().StatusValue
                })
                .AsQueryable();

            var getOrders =
                    orders
                    .Where(a => a.CustomerName.ToLower().Contains(request.searchValue) || a.InvoiceNo.ToLower().Contains(request.searchValue))
                    .OrderBy($"{request.sortColumn} {request.sortOrder}");

            var data = await PaginatedList<OrderDto>.CreateAsync(getOrders, request.page ?? 1, request.length);
            return data;
        }
    }
}
