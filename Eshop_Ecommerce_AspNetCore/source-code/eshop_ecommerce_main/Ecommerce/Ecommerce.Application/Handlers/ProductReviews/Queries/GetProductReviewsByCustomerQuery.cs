using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.ProductReviews.Queries
{
    public class GetProductReviewsByCustomerQuery : IRequest<List<ProductReviewDto>>
    {
        public long CustomerId { get; set; }
    }
    public class GetProductReviewsByCustomerQueryHandler : IRequestHandler<GetProductReviewsByCustomerQuery, List<ProductReviewDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;

        public GetProductReviewsByCustomerQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<ProductReviewDto>> Handle(GetProductReviewsByCustomerQuery request, CancellationToken cancellationToken)
        {
            var customerReviews = await _db.ProductReviews.Include(c => c.Product).Include(c => c.Customer)
                .Where(o => o.CustomerId == request.CustomerId).ToListAsync(cancellationToken);

            var customerReviewsDto = _mapper.Map<List<ProductReviewDto>>(customerReviews);
            return customerReviewsDto;
        }
    }
}
