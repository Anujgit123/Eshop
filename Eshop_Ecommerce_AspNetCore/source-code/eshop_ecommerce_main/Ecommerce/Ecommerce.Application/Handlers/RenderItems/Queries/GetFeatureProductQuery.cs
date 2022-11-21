using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Helpers;
using Ecommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace Ecommerce.Application.Handlers.RenderItems.Queries
{
    public class GetFeatureProductQuery : IRequest<IEnumerable<FeatureProductShocaseDto>>
    {
    }
    public class GetFeatureProductQueryHandler : IRequestHandler<GetFeatureProductQuery, IEnumerable<FeatureProductShocaseDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        private readonly IKeyAccessor _keyAccessor;
        public GetFeatureProductQueryHandler(IDataContext db, IMapper mapper, IKeyAccessor keyAccessor)
        {
            _db = db;
            _mapper = mapper;
            _keyAccessor = keyAccessor;
        }

        public async Task<IEnumerable<FeatureProductShocaseDto>> Handle(GetFeatureProductQuery request, CancellationToken cancellationToken)
        {
            List<FeatureProductShocaseDto> conFeatProduct = JsonSerializer.Deserialize<List<FeatureProductShocaseDto>>(_keyAccessor.GetSection("FeatureProductConfiguration"));

            var featureVarients = (from pv in _db.Variants
                                   join vi in _db.VariantImages on pv.Id equals vi.VariantId into vilist
                                   from vi in vilist.DefaultIfEmpty()
                                   select new FeatureProductShocaseVarientDto
                                   {
                                       Id = pv.Id,
                                       Title = pv.Title,
                                       ProductId = pv.ProductId,
                                       Sku = pv.Sku,
                                       Price = pv.Price,
                                   }).AsQueryable();



            List<FeatureProductShocaseDto> productShocases = await (from p in _db.Products.Include(o => o.Category)
                                                            .Where(o => conFeatProduct.Select(o => o.ProductId).Contains(o.Id))
                                                                    join pi in _db.ProductImages on p.Id equals pi.ProductId into plist
                                                                    from pi in plist.DefaultIfEmpty()
                                                                    join i in _db.Galleries on pi.ImageId equals i.Id into ilist
                                                                    from i in ilist.DefaultIfEmpty()

                                                                    select new FeatureProductShocaseDto
                                                                    {
                                                                        ProductId = p.Id,
                                                                        CategoryId = p.CategoryId,
                                                                        Name = p.Name,
                                                                        ProductImagePreview = i.Name,
                                                                        Price = (featureVarients != null ? (MinMaxVal.getMinMaxVal(featureVarients.Where(o => o.ProductId == p.Id).Select(o => o.Price).AsQueryable().ToArray())) : null),
                                                                        FeatureVarient = (featureVarients != null ? featureVarients.FirstOrDefault(o => o.ProductId == p.Id) : null)

                                                                    }).ToListAsync();


            productShocases = (from p in productShocases
                               join fp in conFeatProduct on p.ProductId equals fp.ProductId into fplist
                               from fp in fplist.DefaultIfEmpty()
                               select new FeatureProductShocaseDto
                               {
                                   ProductId = p.ProductId,
                                   CategoryId = p.CategoryId,
                                   Name = p.Name,
                                   ProductImagePreview = p.ProductImagePreview,
                                   Price = p.Price,
                                   FeatureVarient = p.FeatureVarient,
                                   Order = fp.Order

                               }).OrderBy(o => o.Order).ToList();



            List<HeaderSliderDto> getheaderSlider = JsonSerializer.Deserialize<List<HeaderSliderDto>>(_keyAccessor.GetSection("HeaderSlider"));
            List<HeaderSliderDto> headerSlider = new List<HeaderSliderDto>();

            if (getheaderSlider != null)
            {
                headerSlider = getheaderSlider;
            }

            return productShocases.Where(o => o.FeatureVarient != null);
        }

    }
}
