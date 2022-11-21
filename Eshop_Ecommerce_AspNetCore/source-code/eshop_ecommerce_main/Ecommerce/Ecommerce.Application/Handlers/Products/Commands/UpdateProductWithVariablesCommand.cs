using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Products.Commands
{
    public class UpdateProductWithVariablesCommand : IRequest<Response<string>>
    {
        public ProductForEditDto ProductForEditDto { get; set; }
    }
    public class UpdateProductWithVariablesCommandHandler : IRequestHandler<UpdateProductWithVariablesCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        public UpdateProductWithVariablesCommandHandler(IDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateProductWithVariablesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var vardel = await _db.Variants.Where(o => o.ProductId == request.ProductForEditDto.ProductId).ToListAsync();
                _db.Variants.RemoveRange(vardel);

                Product product = await _db.Products.FindAsync(request.ProductForEditDto.ProductId);
                product.CategoryId = request.ProductForEditDto.CategoryId;
                product.ShortDescription = request.ProductForEditDto.ShortDescription;
                product.Description = request.ProductForEditDto.Description;
                product.VariableTheme = request.ProductForEditDto.VariableTheme;
                product.Name = request.ProductForEditDto.Name;
                _db.Products.Update(product);



                if (request.ProductForEditDto.ProductImage != null)
                {
                    ProductImage productImage = new ProductImage();
                    var proImaRemove = await _db.ProductImages.Where(o => o.ProductId == request.ProductForEditDto.ProductId).ToListAsync();
                    _db.ProductImages.RemoveRange(proImaRemove);

                    productImage.ProductId = request.ProductForEditDto.ProductId;
                    productImage.ImageId = request.ProductForEditDto.ProductImage;
                    _db.ProductImages.Add(productImage);
                }

                if (request.ProductForEditDto.ProductVarient != null)
                {
                    foreach (var item in request.ProductForEditDto.ProductVarient)
                    {

                        VariantImage variantImage = new VariantImage();
                        var provarImaRemove = await _db.VariantImages.Where(o => o.VariantId == item.Id).ToListAsync();
                        _db.VariantImages.RemoveRange(provarImaRemove);

                        //var variantId = Guid.NewGuid().ToString();

                        var isvariant = await _db.Variants.Where(o => o.Id == item.Id).FirstOrDefaultAsync();
                        if (isvariant != null)
                        {
                            isvariant.ProductId = request.ProductForEditDto.ProductId;
                            isvariant.Title = item.Title;
                            isvariant.SizeId = item.SizeId;
                            isvariant.ColorId = item.ColorId;
                            isvariant.Price = item.Price;
                            isvariant.Sku = item.Sku;
                            isvariant.Quantity = (int)item.Quantity;

                            _db.Variants.Update(isvariant);

                            if (item.VarientImageId != null)
                            {
                                variantImage.VariantId = item.Id;
                                variantImage.ImageId = item.VarientImageId;
                                await _db.VariantImages.AddAsync(variantImage);
                            }
                        }
                        else
                        {
                            Variant variant = new Variant();
                            //variant.Id = variantId;
                            variant.ProductId = request.ProductForEditDto.ProductId;
                            variant.Title = item.Title;
                            variant.SizeId = item.SizeId;
                            variant.ColorId = item.ColorId;
                            variant.Price = item.Price;
                            variant.Sku = item.Sku;
                            variant.Quantity = (int)item.Quantity;

                            await _db.Variants.AddAsync(variant);
                            await _db.SaveChangesAsync();

                            if (item.VarientImageId != null)
                            {
                                variantImage.VariantId = variant.Id;
                                variantImage.ImageId = item.VarientImageId;
                                _db.VariantImages.Add(variantImage);
                            }
                        }
                    }
                }
                else
                {
                    var allvariant = await _db.Variants.Where(o => o.ProductId == request.ProductForEditDto.ProductId).ToListAsync();
                    _db.Variants.RemoveRange(allvariant);
                }

                await _db.SaveChangesAsync();
                return Response<string>.Success(product.Name, "Successfully updated the category");
            }
            catch (Exception e)
            {
                return Response<string>.Fail("Failed to add the category");
            }
        }
    }
}
