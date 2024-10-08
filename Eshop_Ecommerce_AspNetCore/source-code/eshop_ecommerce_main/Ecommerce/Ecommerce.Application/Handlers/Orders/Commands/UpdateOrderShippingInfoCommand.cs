﻿using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Orders.Commands
{
    public class UpdateOrderShippingInfoCommand : IRequest<Response<string>>
    {
        public UpdateOrderShippingInfoDto UpdateOrderShipping { get; set; }
    }
    public class UpdateOrderShippingInfoCommandHandler : IRequestHandler<UpdateOrderShippingInfoCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public UpdateOrderShippingInfoCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateOrderShippingInfoCommand request, CancellationToken cancellationToken)
        {
            var order = await _db.Orders.FindAsync(request.UpdateOrderShipping.OrderId);
            order.CustomerName = request.UpdateOrderShipping.CustomerName;
            order.CustomerMobile = request.UpdateOrderShipping.CustomerMobile;
            order.CustomerEmail = request.UpdateOrderShipping.CustomerEmail;
            order.CustomerAddress = request.UpdateOrderShipping.CustomerAddress;
            order.CustomerComment = request.UpdateOrderShipping.CustomerComment;

            try
            {
                _db.Orders.Update(order);
                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success("Successfully updated");
            }
            catch (Exception e)
            {
                return Response<string>.Fail("Failed to update");
            }
        }
    }
}
