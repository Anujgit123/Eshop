using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.CustomerAccount.Queries
{
    public class GetCustomerOrdersQuery : IRequest<List<CustomerOrderDto>>
    {
    }
    public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, List<CustomerOrderDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        public GetCustomerOrdersQueryHandler(IDataContext db, IMapper mapper, ICurrentUser currentUser)
        {
            _db = db;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<List<CustomerOrderDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _db.Orders.AsNoTracking().Include(o => o.OrderDetails)
                .Include(o => o.OrderStatus)
                .ThenInclude(o => o.OrderStatusValue)
                //.Include(o => o.ShippingArea)
                .Where(o => o.CreatedBy == _currentUser.UserName)
                .Select(o => new CustomerOrderDto
                {
                    OrderId = o.Id,
                    InvoiceNo = o.InvoiceNo,
                    OrderDate = (DateTime)o.CreatedDate,
                    OrderAmount = o.OrderAmount + (decimal)o.DeliveryCharge,
                    OrderStatus = o.OrderStatus.OrderByDescending(o => o.Id).Select(o => o.OrderStatusValue).AsQueryable().FirstOrDefault().StatusValue
                })
                .ToListAsync();

            var data = orders;
            return data;
        }
    }
}
