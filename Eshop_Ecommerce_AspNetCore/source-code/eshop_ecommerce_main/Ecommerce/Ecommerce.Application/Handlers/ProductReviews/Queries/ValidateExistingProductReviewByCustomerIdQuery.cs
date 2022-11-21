using AutoMapper;
using Ecommerce.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.ProductReviews.Queries
{
    public class ValidateExistingProductReviewByCustomerIdQuery : IRequest<bool>
    {
        public long CustomerId { get; set; }
        public int ProductId { get; set; }
    }
    public class ValidateExistingProductReviewByCustomerIdQueryHandler : IRequestHandler<ValidateExistingProductReviewByCustomerIdQuery, bool>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        //private readonly UserManager<ApplicationUser> _userManager;
        public ValidateExistingProductReviewByCustomerIdQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ValidateExistingProductReviewByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var result = _db.ProductReviews.Include(c => c.Customer)
                .Any(c => c.CustomerId == request.CustomerId && c.ProductId == request.ProductId);

            return result;
        }
    }
}
