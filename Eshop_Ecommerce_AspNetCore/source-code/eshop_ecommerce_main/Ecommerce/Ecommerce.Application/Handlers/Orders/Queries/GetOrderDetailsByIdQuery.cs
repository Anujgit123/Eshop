using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Orders.Queries
{
    public class GetOrderDetailsByIdQuery : IRequest<OrderDetailsDto>
    {
        public int OrderId { get; set; }
    }
    public class GetOrderDetailsByIdQueryHandler : IRequestHandler<GetOrderDetailsByIdQuery, OrderDetailsDto>
    {
        private readonly IDataContext _db;
        private readonly IUserService _user;
        private readonly IMapper _mapper;
        public GetOrderDetailsByIdQueryHandler(IDataContext db, IUserService user, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _user = user;
        }

        public async Task<OrderDetailsDto> Handle(GetOrderDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var CurrentOrderStatus = await _db.OrderStatus.AsNoTracking()
                .Include(o => o.OrderStatusValue)
                .OrderByDescending(o => o.Id)
                .FirstOrDefaultAsync(e => e.OrderId == request.OrderId);

            var orderById = await _db.Orders.AsNoTracking().Include(o => o.OrderDetails)
                .Include(o => o.OrderStatus)
                .ThenInclude(o => o.OrderStatusValue)
                .Include(o => o.DeliveryMethod)
                .Include(o => o.OrderPayments)
                .FirstOrDefaultAsync(e => e.Id == request.OrderId);

            var orderItemDetails = await (from od in _db.OrderDetails
                                          join pv in _db.Variants on od.ProductVariantId equals pv.Id into pvlist
                                          from pv in pvlist.DefaultIfEmpty()
                                          join pvi in _db.VariantImages on pv.Id equals pvi.VariantId into pvilist
                                          from pvi in pvilist.DefaultIfEmpty()
                                          join i in _db.Galleries on pvi.ImageId equals i.Id into ilist
                                          from i in ilist.DefaultIfEmpty()
                                          where od.OrderId == request.OrderId
                                          select new OrderItemDetailsDto
                                          {
                                              ProductId = pv.ProductId,
                                              ProductVariantId = pv.Id,
                                              OrderItemTitle = od.ProductName,
                                              OrderItemImage = i.Name,
                                              ItemUnitPrice = od.UnitPrice,
                                              ItemQty = od.Qty,
                                          }).ToListAsync();

            UpdateOrderStatusDto updateOrderStatus = new UpdateOrderStatusDto();
            updateOrderStatus.OrderId = request.OrderId;
            updateOrderStatus.InvoiceNo = orderById.InvoiceNo;
            updateOrderStatus.NewOrderStatus = (int)CurrentOrderStatus.OrderStatusValueId;
            if (CurrentOrderStatus != null && CurrentOrderStatus.OrderStatusValue != null)
            {
                updateOrderStatus.CurrentOrderStatus = CurrentOrderStatus.OrderStatusValue.StatusValue;
            }

            var orderStatus = await _db.OrderStatus.AsNoTracking().Include(o => o.OrderStatusValue).Where(o => o.OrderId == request.OrderId).OrderBy(o => o.Id).ToListAsync();
            var user = await _user.GetUserByUserNameAsync(orderById.UserId);
            var customer = await _db.Customers.Where(o => o.ApplicationUserId == user.Data.Id).FirstOrDefaultAsync();
            var customerInfo = _mapper.Map<CustomerDto>(customer);
            var orderDetails = new OrderDetailsDto
            {
                Order = orderById,
                CurrentOrderStatus = CurrentOrderStatus,
                OrderStatus = orderStatus,
                OrderItemDetails = orderItemDetails,
                UpdateOrderStatus = updateOrderStatus,
                CustomerInfo = customerInfo
            };

            return orderDetails;
        }
    }
}
