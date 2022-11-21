using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Customers.Commands
{
    public class UpdateCustomerAccountInfoCommand : IRequest<Response<string>>
    {
        public CustomerAccountInfoDto CustomerAccountInfo { get; set; }
    }
    public class UpdateCustomerAccountInfoCommandHandler : IRequestHandler<UpdateCustomerAccountInfoCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly ICurrentUser _currentUser;
        public UpdateCustomerAccountInfoCommandHandler(IDataContext db, ICurrentUser currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<Response<string>> Handle(UpdateCustomerAccountInfoCommand request, CancellationToken cancellationToken)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(o => o.ApplicationUserId == _currentUser.UserId);

            if (customer != null)
            {
                customer.FullName = request.CustomerAccountInfo.FullName;
                customer.Phone = request.CustomerAccountInfo.Phone;
                customer.Email = request.CustomerAccountInfo.Email;
                _db.Customers.Update(customer);
            }
            else
            {
                var newCustomer = new Customer
                {
                    ApplicationUserId = _currentUser.UserId,
                    FullName = request.CustomerAccountInfo.FullName,
                    Phone = request.CustomerAccountInfo.Phone,
                    Email = request.CustomerAccountInfo.Email
                };
                await _db.Customers.AddAsync(newCustomer);
            }

            try
            {

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
