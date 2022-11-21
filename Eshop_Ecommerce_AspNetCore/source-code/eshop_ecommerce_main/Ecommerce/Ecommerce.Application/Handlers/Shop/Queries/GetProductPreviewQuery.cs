using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Helpers;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Identity.Entities;
using Ecommerce.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Shop.Queries
{
    public class GetProductPreviewQuery : IRequest<ShopDetailsDto>
    {
        public int Id { get; set; }
    }
    public class GetProductPreviewQueryHandler : IRequestHandler<GetProductPreviewQuery, ShopDetailsDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        private readonly IKeyAccessor _keyAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetProductPreviewQueryHandler(IDataContext db, IMapper mapper, IKeyAccessor keyAccessor, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _mapper = mapper;
            _keyAccessor = keyAccessor;
            _userManager = userManager;
        }

        public async Task<ShopDetailsDto> Handle(GetProductPreviewQuery request, CancellationToken cancellationToken)
        {
            StockConfiguration conStock = JsonSerializer.Deserialize<StockConfiguration>(_keyAccessor.GetSection("StockConfiguration"));

            var productDetailsVarient = (from pv in _db.Variants
                                         join vi in _db.VariantImages on pv.Id equals vi.VariantId into vilist
                                         from vi in vilist.DefaultIfEmpty()
                                         join i in _db.Galleries on vi.ImageId equals i.Id into ilist
                                         from i in ilist.DefaultIfEmpty()
                                         join c in _db.Colors on pv.ColorId equals c.Id into clist
                                         from c in clist.DefaultIfEmpty()
                                         join s in _db.Sizes on pv.SizeId equals s.Id into slist
                                         from s in slist.DefaultIfEmpty()
                                         where pv.ProductId == request.Id
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


            var reviews = (from p in _db.ProductReviews.Include(c => c.Customer)
                           where p.ProductId == request.Id && p.IsActive == true
                           select new ProductReviewsDto
                           {
                               Id = p.Id,
                               Comment = p.Comment,
                               Rating = p.Rating,
                               DateCommented = p.DateCommented,
                               CustomerName = p.Customer.FullName,
                               Reply = p.Reply,
                               DateReplied = p.DateReplied
                           }).OrderByDescending(o => o.DateCommented).AsQueryable();

            if (conStock?.IsOutOfStockItemHidden == true) productDetailsVarient = productDetailsVarient.Where(o => o.Quantity > conStock.OutOfStockThreshold).AsQueryable();

            var availableColor = _mapper.Map<List<ColorDto>>(productDetailsVarient.Select(o => o.Color).Distinct().ToList());
            var availableSize = _mapper.Map<List<SizeDto>>(productDetailsVarient.Select(o => o.Size).Distinct().ToList());

            ProductDetailsDto productDetails = await (from p in _db.Products.Include(o => o.ProductReviews)
                                                      where p.Id == request.Id
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
                                                          Price = (productDetailsVarient != null ? (MinMaxVal.getMinMaxVal(productDetailsVarient.Where(o => o.ProductId == p.Id).Select(o => o.Price).AsQueryable().ToArray())) : null),
                                                          FeatureVarient = (productDetailsVarient != null ? productDetailsVarient.FirstOrDefault(o => o.ProductId == p.Id) : null),
                                                          Varient = productDetailsVarient.ToList(),
                                                          CustomerReviews = reviews.ToList()
                                                      }).FirstOrDefaultAsync();

            return new ShopDetailsDto { ProductDetails = productDetails, AvailableColors = availableColor, AvailableSizes = availableSize };
        }
    }
}
