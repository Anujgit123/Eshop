using Ecommerce.Application.Common;
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
    public class UpdateBillingAddressCommand : IRequest<Response<string>>
    {
        public string BillingAddress { get; set; }
    }
    public class UpdateBillingAddressCommandHandler : IRequestHandler<UpdateBillingAddressCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly ICurrentUser _currentUser;
        public UpdateBillingAddressCommandHandler(IDataContext db, ICurrentUser currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<Response<string>> Handle(UpdateBillingAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(o => o.ApplicationUserId == _currentUser.UserId);

            if (customer != null)
            {
                customer.BillingAddress = request.BillingAddress;
                _db.Customers.Update(customer);
            }
            else
            {
                var newCustomer = new Customer();
                newCustomer.ApplicationUserId = _currentUser.UserId;
                newCustomer.BillingAddress = request.BillingAddress;
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
