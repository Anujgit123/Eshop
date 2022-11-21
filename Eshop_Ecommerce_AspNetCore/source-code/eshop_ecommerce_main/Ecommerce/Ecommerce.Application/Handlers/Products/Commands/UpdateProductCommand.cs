using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Products.Commands
{
    public class UpdateProductCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string VariableTheme { get; set; }
        public long CategoryId { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _db.Products.FindAsync(request.Id);
                _mapper.Map(request, product);
                var updateproduct = _db.Products.Update(product);
                await _db.SaveChangesAsync(cancellationToken);
                var productdto = _mapper.Map<ProductDto>(updateproduct);
                return Response<string>.Success(product.Name, "Successfully updated the category");
            }
            catch (Exception e)
            {
                return Response<string>.Fail("Failed to add the category");
            }
        }
    }
}
