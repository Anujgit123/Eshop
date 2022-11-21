using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Inventory.Queries
{
    public class GetProductStockHistoryWithPagingQuery : IRequest<PaginatedList<StockDto>>
    {
        public int? page { get; set; }
        public int length { get; set; }
        public string searchValue { get; set; } = "";
        public string sortColumn { get; set; } = "Id";
        public string sortOrder { get; set; } = "Desc";
    }
    public class GetProductStockHistoryWithPagingQueryHandler : IRequestHandler<GetProductStockHistoryWithPagingQuery, PaginatedList<StockDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetProductStockHistoryWithPagingQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaginatedList<StockDto>> Handle(GetProductStockHistoryWithPagingQuery request, CancellationToken cancellationToken)
        {
            var stocks = _db.Stocks.Include(o => o.Variant).OrderByDescending(o => o.LastModifiedDate).AsQueryable();

            var getstocks =
                    stocks
                    .Where(a => a.Variant.Title.ToLower().Contains(request.searchValue))
                    .OrderBy($"{request.sortColumn} {request.sortOrder}").ProjectTo<StockDto>(_mapper.ConfigurationProvider); ;


            var data = await PaginatedList<StockDto>.CreateAsync(getstocks, request.page ?? 1, request.length);

            return data;
        }
    }
}
