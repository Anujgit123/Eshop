using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Configuration.Queries
{
    public class GetStockConfigurationQuery : IRequest<StockConfigurationDto>
    {
    }
    public class GetStockConfigurationQueryHandler : IRequestHandler<GetStockConfigurationQuery, StockConfigurationDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetStockConfigurationQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<StockConfigurationDto> Handle(GetStockConfigurationQuery request, CancellationToken cancellationToken)
        {
            var getStockConfiguration = await _db.AppConfigurations.Where(o => o.Key == AppConfigurationType.StockConfiguration).FirstOrDefaultAsync();
            StockConfigurationDto stockConfigurationDto = new StockConfigurationDto();
            if (getStockConfiguration != null)
            {
                stockConfigurationDto = JsonSerializer.Deserialize<StockConfigurationDto>(getStockConfiguration.Value);
            }
            return stockConfigurationDto;
        }

    }
}
