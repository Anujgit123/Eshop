using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Extension;
using Ecommerce.Application.Helpers;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Shop.Queries
{
    public class GetShopByCategoryWithPagingQuery : IRequest<ShopDto>
    {
        public string[] Slug { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public int? page { get; set; }
        public int? pageSize { get; set; }
        public string sortColumn { get; set; } = "ProductId";
        public string sortOrder { get; set; } = "Desc";
    }
    public class GetShopByCategoryWithPagingQueryHandler : IRequestHandler<GetShopByCategoryWithPagingQuery, ShopDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        private readonly IKeyAccessor _keyAccessor;
        public GetShopByCategoryWithPagingQueryHandler(IDataContext db, IMapper mapper, IKeyAccessor keyAccessor)
        {
            _db = db;
            _mapper = mapper;
            _keyAccessor = keyAccessor;
        }

        public async Task<ShopDto> Handle(GetShopByCategoryWithPagingQuery request, CancellationToken cancellationToken)
        {
            StockConfiguration conStock = _keyAccessor["StockConfiguration"] != null ? JsonSerializer.Deserialize<StockConfiguration>(_keyAccessor.GetSection("StockConfiguration")) : new StockConfiguration();

            var productIds = conStock.IsOutOfStockItemHidden ?
                _db.Variants.Where(o => o.Quantity > conStock.OutOfStockThreshold).OrderByDescending(c => c.ProductId).Select(c => c.ProductId).Distinct() :
                _db.Variants.OrderByDescending(c => c.ProductId).Select(c => c.ProductId).Distinct();

            var products = (from p in _db.Products.Include(o => o.Category)
                            join pi in _db.ProductImages on p.Id equals pi.ProductId into plist
                            from pi in plist.DefaultIfEmpty()
                            join i in _db.Galleries on pi.ImageId equals i.Id into ilist
                            from i in ilist.DefaultIfEmpty()
                            where productIds.Contains(p.Id)
                            && request.Slug.Contains(p.Category.Slug)
                            select new ProductShocaseDto
                            {
                                ProductId = p.Id,
                                CategoryId = p.CategoryId,
                                Name = p.Name,
                                Description = p.Description,
                                VariableTheme = p.VariableTheme,
                                ProductImage = pi.ImageId,
                                ProductImagePreview = i.Name,
                            });

            var allProduct = products
                .OrderBy($"{request.sortColumn} {request.sortOrder}")
                .DistinctBy(o => o.ProductId).AsQueryable();


            var data = PaginatedList<ProductShocaseDto>.Create(allProduct, request.page ?? 1, request.pageSize ?? 9);

            var availableProduct = data.Items;
            var productId = availableProduct.Select(c => c.ProductId);

            var variants = await (from pv in _db.Variants
                                  join vi in _db.VariantImages on pv.Id equals vi.VariantId into vilist
                                  from vi in vilist.DefaultIfEmpty()
                                  join i in _db.Galleries on vi.ImageId equals i.Id into ilist
                                  from i in ilist.DefaultIfEmpty()
                                  join c in _db.Colors on pv.ColorId equals c.Id into clist
                                  from c in clist.DefaultIfEmpty()
                                  join s in _db.Sizes on pv.SizeId equals s.Id into slist
                                  from s in slist.DefaultIfEmpty()
                                  where productId.Contains(pv.ProductId) &&
                                  (String.IsNullOrEmpty(request.size) ? true : (pv.SizeId.ToString() == request.size)) &&
                                  (String.IsNullOrEmpty(request.color) ? true : (pv.ColorId.ToString() == request.color))
                                  select new ProductShocaseVarientDto
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

            if (conStock.IsOutOfStockItemHidden) variants = variants.Where(c => c.Quantity > conStock.OutOfStockThreshold).ToList();

            data.Items.ForEach(c =>
            {
                c.Price = (variants != null
                    ? (MinMaxVal.getMinMaxVal(variants.Where(o => o.ProductId == c.ProductId).Select(o => o.Price).AsQueryable().ToArray()))
                    : null);
                c.AvailableColorVarient = _mapper.Map<List<ColorDto>>(variants != null
                    ? variants.Where(o => o.ProductId == c.ProductId).Select(o => o.Color).Where(o => o != null).OrderBy(o => o.Id).ToList()
                    : null);
                c.AvailableSizesVarient = _mapper.Map<List<SizeDto>>(variants != null
                    ? variants.Where(o => o.ProductId == c.ProductId).Select(o => o.Size).Where(o => o != null).OrderBy(o => o.Id).ToList()
                    : null);
                c.FeatureVarient = (variants != null
                    ? variants.FirstOrDefault(o => o.ProductId == c.ProductId)
                    : null);
            });

            var filteredData = data.Items.Where(c => c.FeatureVarient != null).ToList();
            data.Items.Clear();
            data.Items.AddRange(filteredData);

            return new ShopDto { PaginatedProductList = data };

        }
    }
}
