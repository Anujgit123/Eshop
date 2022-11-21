using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Products.Queries
{
    public class GetProductWithVariablesByIdQuery : IRequest<ProductForEditDto>
    {
        public int Id { get; set; }
    }
    public class GetProductWithVariablesByIdQueryHandler : IRequestHandler<GetProductWithVariablesByIdQuery, ProductForEditDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetProductWithVariablesByIdQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductForEditDto> Handle(GetProductWithVariablesByIdQuery request, CancellationToken cancellationToken)
        {
            //var product = await _db.Products.FindAsync(request.Id);
            //var result = _mapper.Map<ProductDto>(product);

            List<ProductVarientForEditDto> productVarientVM = await (from pv in _db.Variants
                                                                     where pv.ProductId == request.Id
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
                                                                     }).ToListAsync();

            ProductForEditDto productvm = await (from p in _db.Products
                                                 where p.Id == request.Id
                                                 join pi in _db.ProductImages on p.Id equals pi.ProductId into plist
                                                 from pi in plist.DefaultIfEmpty()
                                                 join i in _db.Galleries on pi.ImageId equals i.Id into ilist
                                                 from i in ilist.DefaultIfEmpty()

                                                 select new ProductForEditDto
                                                 {
                                                     ProductId = p.Id,
                                                     CategoryId = p.CategoryId,
                                                     Name = p.Name,
                                                     ShortDescription = p.ShortDescription,
                                                     Description = p.Description,
                                                     VariableTheme = p.VariableTheme,
                                                     ProductImage = pi.ImageId,
                                                     ProductImagePreview = i.Name,
                                                     ProductVarient = productVarientVM.Count != 0 ? productVarientVM : null

                                                 }).FirstOrDefaultAsync();

            return productvm;


        }
    }
}
