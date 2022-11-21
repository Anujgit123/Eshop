using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Customers.Queries
{
    public class GetCustomerProfileByIdQuery : IRequest<CustomerProfileDto>
    {
        public long Id { get; set; }
    }
    public class GetCustomerProfileByIdQueryHandler : IRequestHandler<GetCustomerProfileByIdQuery, CustomerProfileDto>
    {
        private readonly IDataContext _db;
        private readonly IUserService _user;
        private readonly IMapper _mapper;
        public GetCustomerProfileByIdQueryHandler(IDataContext db, IUserService user, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _user = user;
        }

        public async Task<CustomerProfileDto> Handle(GetCustomerProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _db.Customers.FindAsync(request.Id);
            var customerInfo = _mapper.Map<CustomerDto>(customer);

            var user = await _user.GetUserByIdAsync(customer.ApplicationUserId);
            var order = await _db.Orders.Where(o => o.UserId == user.Data.UserName).ToListAsync();
            var orderIds = order.Select(o => o.Id);
            var orderStatus = await _db.OrderStatus.Include(o => o.OrderStatusValue).Where(o => orderIds.Contains(o.OrderId)).ToListAsync();


            var orderDto = _mapper.Map<List<OrderDto>>(order);
            orderDto.ForEach(o =>
            {
                o.CurrentOrderStatus = orderStatus.Where(c => c.OrderId == o.Id).OrderByDescending(o => o.Id).Select(p => p.OrderStatusValue.StatusValue).FirstOrDefault();
            });
            var customerProfile = new CustomerProfileDto();
            customerProfile.CustomerInfo = customerInfo;
            customerProfile.CustomerInfo.Gender = user.Data.Gender;
            customerProfile.TotalOrder = order.Count();

            var orderAmount = order.Sum(o => o.OrderAmount);
            var deliveryCharge = order.Sum(o => o.DeliveryCharge ?? 0M);

            customerProfile.TotalOrderAmount = orderAmount;
            customerProfile.TotalDeliveryCharge = deliveryCharge;
            customerProfile.TotalAmount = orderAmount + deliveryCharge;
            customerProfile.Orders = orderDto;
            return customerProfile;
        }
    }
}
