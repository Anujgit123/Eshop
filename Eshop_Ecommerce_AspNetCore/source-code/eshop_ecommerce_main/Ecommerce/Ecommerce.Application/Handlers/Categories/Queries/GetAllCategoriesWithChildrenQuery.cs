using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Categories.Queries
{
    public class GetAllCategoriesWithChildrenQuery : IRequest<List<CategoryDto>>
    {
    }
    public class GetAllCategoryWithChildrenQueryHandler : IRequestHandler<GetAllCategoriesWithChildrenQuery, List<CategoryDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetAllCategoryWithChildrenQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesWithChildrenQuery request, CancellationToken cancellationToken)
        {
            var category = await _db.Categories
                .Include(o => o.Children)
                .ToListAsync();

            var result = _mapper.Map<List<CategoryDto>>(category);
            return result;
        }
    }
}
