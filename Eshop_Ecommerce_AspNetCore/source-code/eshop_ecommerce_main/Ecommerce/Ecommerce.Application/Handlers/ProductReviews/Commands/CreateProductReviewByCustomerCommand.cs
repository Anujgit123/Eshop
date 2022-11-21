using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Handlers.Customers.Queries;
using Ecommerce.Application.Handlers.ProductReviews.Queries;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.ProductReviews.Commands
{
    public class CreateProductReviewByCustomerCommand : IRequest<Response<string>>
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
    }
    public class CreateProductReviewByCustomerCommandHandler : IRequestHandler<CreateProductReviewByCustomerCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IMediator _mediator;
        public CreateProductReviewByCustomerCommandHandler(IDataContext db, IMapper mapper, ICurrentUser currentUser, IMediator mediator)
        {
            _db = db;
            _mapper = mapper;
            _currentUser = currentUser;
            _mediator = mediator;
        }
        public async Task<Response<string>> Handle(CreateProductReviewByCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _mediator.Send(new GetCustomerIdByUserIdQuery { Id = _currentUser.UserId }, cancellationToken);
            var isAlreadyReviewed = await _mediator.Send(new ValidateExistingProductReviewByCustomerIdQuery
            { CustomerId = customer, ProductId = request.ProductId });

            if (isAlreadyReviewed) return Response<string>.Fail("Sorry! You must order this item first or you have already reviewed this product.");
            try
            {
                var productReview = _mapper.Map<ProductReview>(request);
                productReview.CustomerId = customer;
                productReview.DateCommented = DateTime.UtcNow;
                productReview.IsActive = true;
                var addReview = await _db.ProductReviews.AddAsync(productReview, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success("Successfully created");
            }
            //catch (ValidationException e)
            //{
            //    Console.WriteLine(e);
            //    return Response<string>.Fail("Failed to add item!");
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Response<string>.Fail("Failed to add item!");
            }
        }
    }
}
