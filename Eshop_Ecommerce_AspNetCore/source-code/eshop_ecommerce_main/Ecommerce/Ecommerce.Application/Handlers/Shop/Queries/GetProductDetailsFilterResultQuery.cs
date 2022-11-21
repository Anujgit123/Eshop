using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Shop.Queries
{
    public class GetProductDetailsFilterResultQuery : IRequest<ProductDetailsFilterResultDto>
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
    }
    public class GetProductDetailsFilterResultQueryHandler : IRequestHandler<GetProductDetailsFilterResultQuery, ProductDetailsFilterResultDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetProductDetailsFilterResultQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDetailsFilterResultDto> Handle(GetProductDetailsFilterResultQuery request, CancellationToken cancellationToken)
        {
            var filterResult = await (from pv in _db.Variants
                                      join vi in _db.VariantImages on pv.Id equals vi.VariantId into vilist
                                      from vi in vilist.DefaultIfEmpty()
                                      join i in _db.Galleries on vi.ImageId equals i.Id into ilist
                                      from i in ilist.DefaultIfEmpty()
                                      join c in _db.Colors on pv.ColorId equals c.Id into clist
                                      from c in clist.DefaultIfEmpty()
                                      where pv.ProductId == request.Id &&
                                    (String.IsNullOrEmpty(request.Size) ? true : (pv.SizeId.ToString() == request.Size))
                                    && (String.IsNullOrEmpty(request.Color) ? true : (pv.ColorId.ToString() == request.Color))
                                      select new ProductDetailsFilterResultDto
                                      {
                                          VarientId = pv.Id,
                                          Sku = pv.Sku,
                                          Quantity = pv.Quantity,
                                          Price = pv.Price.ToString(),
                                          VarientImage = i.Name
                                      }).FirstOrDefaultAsync();


            return filterResult;
        }
    }
}
