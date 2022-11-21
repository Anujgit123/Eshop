using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Products.Queries
{
    public class GetProductVariablesByIdQuery : IRequest<ProductVarientForEditDto>
    {
        public int Id { get; set; }
    }
    public class GetProductVariablesByIdQueryHandler : IRequestHandler<GetProductVariablesByIdQuery, ProductVarientForEditDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetProductVariablesByIdQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductVarientForEditDto> Handle(GetProductVariablesByIdQuery request, CancellationToken cancellationToken)
        {

            ProductVarientForEditDto productVarientVm = await (from pv in _db.Variants
                                                               where pv.Id == request.Id
                                                               join vi in _db.VariantImages on pv.Id equals vi.VariantId into vilist
                                                               from vi in vilist.DefaultIfEmpty()
                                                               join i in _db.Galleries on vi.ImageId equals i.Id into ilist
                                                               from i in ilist.DefaultIfEmpty()

                                                               select new ProductVarientForEditDto
                                                               {
                                                                   Id = pv.Id,
                                                                   Title = pv.Title,
                                                                   ProductId = pv.ProductId,
                                                                   SizeId = pv.SizeId,
                                                                   ColorId = pv.ColorId,
                                                                   Sku = pv.Sku,
                                                                   Price = pv.Price,
                                                                   Quantity = pv.Quantity,
                                                                   VarientImageId = vi.ImageId == null ? null : vi.ImageId,
                                                                   VarientImagePreview = i.Name == null ? null : i.Name
                                                               }).FirstOrDefaultAsync();


            return productVarientVm;


        }
    }
}
