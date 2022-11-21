using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.RenderItems.Queries
{
    public class GetProductQuickViewQuery : IRequest<ShopDetailsDto>
    {
        public int ProductId { get; set; }
    }
    public class GetProductQuickViewQueryHandler : IRequestHandler<GetProductQuickViewQuery, ShopDetailsDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetProductQuickViewQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ShopDetailsDto> Handle(GetProductQuickViewQuery request, CancellationToken cancellationToken)
        {
            var productVarients = (from pv in _db.Variants
                                   join vi in _db.VariantImages on pv.Id equals vi.VariantId into vilist
                                   from vi in vilist.DefaultIfEmpty()
                                   join i in _db.Galleries on vi.ImageId equals i.Id into ilist
                                   from i in ilist.DefaultIfEmpty()
                                   join c in _db.Colors on pv.ColorId equals c.Id into clist
                                   from c in clist.DefaultIfEmpty()
                                   join s in _db.Sizes on pv.SizeId equals s.Id into slist
                                   from s in slist.DefaultIfEmpty()
                                   where pv.ProductId == request.ProductId


                                   select new ProductDetailsVarientDto
                                   {
                                       Id = pv.Id,
                                       Title = pv.Title,
                                       ProductId = pv.ProductId,
                                       Size = s,
                                       Color = c,
                                       Sku = pv.Sku,
                                       Price = pv.Price,
                                       Quantity = pv.Quantity,
                                       VarientImagePreview = i.Name ?? null
                                   }).AsQueryable();

            var availableColor = _mapper.Map<List<ColorDto>>(productVarients.Select(o => o.Color).Distinct().ToList());
            var availableSize = _mapper.Map<List<SizeDto>>(productVarients.Select(o => o.Size).Distinct().ToList());


            ProductDetailsDto productDetails = await (from p in _db.Products
                                                      where p.Id == request.ProductId
                                                      join pi in _db.ProductImages on p.Id equals pi.ProductId into plist
                                                      from pi in plist.DefaultIfEmpty()
                                                      join i in _db.Galleries on pi.ImageId equals i.Id into ilist
                                                      from i in ilist.DefaultIfEmpty()

                                                      select new ProductDetailsDto
                                                      {
                                                          ProductId = p.Id,
                                                          CategoryId = p.CategoryId,
                                                          Name = p.Name,
                                                          ShortDescription = p.ShortDescription,
                                                          Description = p.Description,
                                                          VariableTheme = p.VariableTheme,
                                                          ProductImage = pi.ImageId,
                                                          ProductImagePreview = i.Name,
                                                          AvailableColorVarient = _mapper.Map<List<ColorDto>>(availableColor),
                                                          AvailableSizesVarient = _mapper.Map<List<SizeDto>>(availableSize),
                                                          Price = (productVarients != null ? (MinMaxVal.getMinMaxVal(productVarients.Where(o => o.ProductId == p.Id).Select(o => o.Price).AsQueryable().ToArray())) : null),
                                                          FeatureVarient = (productVarients != null ? productVarients.FirstOrDefault(o => o.ProductId == p.Id) : null),
                                                          Varient = productVarients.ToList()
                                                      }).FirstOrDefaultAsync();

            return new ShopDetailsDto { ProductDetails = productDetails, AvailableColors = availableColor, AvailableSizes = availableSize };
        }
    }
}