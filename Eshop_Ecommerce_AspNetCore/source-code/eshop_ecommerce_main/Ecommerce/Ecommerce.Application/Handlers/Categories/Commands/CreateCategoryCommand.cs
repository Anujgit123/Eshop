using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public long? ParentCategoryId { get; set; }
        public string Slug { get; set; }
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            request.Slug = request.Slug == null ?
                request.Name.ToLower().Replace(" ", "-") :
                request.Slug = request.Slug.ToLower().Replace(" ", "-");

            try
            {
                var category = _mapper.Map<Category>(request);
                var addcategory = await _db.Categories.AddAsync(category);
                await _db.SaveChangesAsync(cancellationToken);
                return Response<string>.Success(category.Name, "Successfully created");
            }
            //catch (ValidationException e)
            //{
            //    Console.WriteLine(e);
            //    return Response<string>.Fail("Failed to add item!");
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Response<string>.Fail("Failed to add item!");
            }
        }
    }
}
