using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Helpers;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Application.Extension;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Stripe;

namespace Ecommerce.Application.Handlers.RenderItems.Queries
{
    public class GetNewProductQuery : IRequest<IEnumerable<NewProductShocaseDto>>
    {
    }
    public class GetNewProductQueryHandler : IRequestHandler<GetNewProductQuery, IEnumerable<NewProductShocaseDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        private readonly IKeyAccessor _keyAccessor;
        public GetNewProductQueryHandler(IDataContext db, IMapper mapper, IKeyAccessor keyAccessor)
        {
            _db = db;
            _mapper = mapper;
            _keyAccessor = keyAccessor;
        }

        public async Task<IEnumerable<NewProductShocaseDto>> Handle(GetNewProductQuery request, CancellationToken cancellationToken)
        {
            StockConfiguration conStock = _keyAccessor["StockConfiguration"] != null ? JsonSerializer.Deserialize<StockConfiguration>(_keyAccessor.GetSection("StockConfiguration")) : new StockConfiguration();

            var productIds = (conStock?.IsOutOfStockItemHidden == true) ?
                _db.Variants.Where(o => o.Quantity > conStock.OutOfStockThreshold).OrderByDescending(c => c.ProductId).Select(c => c.ProductId).Distinct() :
                _db.Variants.OrderByDescending(c => c.ProductId).Select(c => c.ProductId).Distinct();


            var newProduct = await (from p in _db.Products.Include(o => o.Category)
                                    join pi in _db.ProductImages on p.Id equals pi.ProductId into plist
                                    from pi in plist.DefaultIfEmpty()
                                    join i in _db.Galleries on pi.ImageId equals i.Id into ilist
                                    from i in ilist.DefaultIfEmpty()
                                    where productIds.Contains(p.Id)
                                    select new NewProductShocaseDto
                                    {
                                        ProductId = p.Id,
                                        CategoryId = p.CategoryId,
                                        Name = p.Name,
                                        Description = p.Description,
                                        VariableTheme = p.VariableTheme,
                                        ProductImage = pi.ImageId,
                                        ProductImagePreview = i.Name,
                                    }).OrderByDescending(o => o.ProductId).Take(4).ToListAsync(cancellationToken);


            var newProductId = newProduct.Select(c => c.ProductId);

            var newProductVariant = await (from pv in _db.Variants
                                           join vi in _db.VariantImages on pv.Id equals vi.VariantId into vilist
                                           from vi in vilist.DefaultIfEmpty()
                                           join i in _db.Galleries on vi.ImageId equals i.Id into ilist
                                           from i in ilist.DefaultIfEmpty()
                                           join c in _db.Colors on pv.ColorId equals c.Id into clist
                                           from c in clist.DefaultIfEmpty()
                                           join s in _db.Sizes on pv.SizeId equals s.Id into slist
                                           from s in slist.DefaultIfEmpty()
                                           where newProductId.Contains(pv.ProductId)
                                           select new NewProductShocaseVarientDto
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
                                           }).ToListAsync(cancellationToken);


            newProduct.ForEach(c =>
            {
                c.Price = (newProductVariant != null
                    ? (MinMaxVal.getMinMaxVal(newProductVariant.Where(o => o.ProductId == c.ProductId)
                        .Select(o => o.Price).AsQueryable().ToArray()))
                    : null);
                c.AvailableColorVarient = _mapper.Map<List<ColorDto>>(newProductVariant != null
                    ? newProductVariant.Where(o => o.ProductId == c.ProductId).Select(o => o.Color).Where(o => o != null)
                        .OrderBy(o => o.Id).ToList()
                    : null);
                c.AvailableSizesVarient = _mapper.Map<List<SizeDto>>(newProductVariant != null
                    ? newProductVariant.Where(o => o.ProductId == c.ProductId).Select(o => o.Size).Where(o => o != null)
                        .OrderBy(o => o.Id).ToList()
                    : null);
                c.FeatureVarient = (newProductVariant != null
                    ? newProductVariant.FirstOrDefault(o => o.ProductId == c.ProductId)
                    : null);

            });

            return newProduct.Where(o => o.FeatureVarient != null);
        }

    }
}
