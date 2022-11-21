using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Categories.Queries
{
    public class GetAllChildrenCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }
    public class GetAllChildrenCategoryByIdQueryHandler : IRequestHandler<GetAllChildrenCategoryByIdQuery, CategoryDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public GetAllChildrenCategoryByIdQueryHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetAllChildrenCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _db.Categories
                .Where(o => o.Id == request.Id)
                .Include(o => o.Children)
                .ThenInclude(o => o.Children)
                .FirstOrDefaultAsync();
            var result = _mapper.Map<CategoryDto>(category);
            return result;
        }
    }
}
