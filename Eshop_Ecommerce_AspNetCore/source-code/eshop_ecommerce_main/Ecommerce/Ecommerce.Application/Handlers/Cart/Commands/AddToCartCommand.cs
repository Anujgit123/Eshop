using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Cart.Commands
{
    public class AddToCartCommand : IRequest<Unit>
    {
        public int VariableId { get; set; }
        public int Qty { get; set; }
    }
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Unit>
    {
        private readonly IDataContext _db;
        private readonly ICookieService _cookie;
        private readonly IMapper _mapper;
        public AddToCartCommandHandler(IDataContext db, IMapper mapper, ICookieService cookie)
        {
            _db = db;
            _mapper = mapper;
            _cookie = cookie;
        }

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            List<CartDto> cart = new List<CartDto>();
            if (_cookie.Contains("shop-cart"))
            {
                cart = JsonSerializer.Deserialize<List<CartDto>>(_cookie.Get("shop-cart"));
            }


            if (cart.Any(o => o.VariableId == request.VariableId))
            {
                var singleCartItem = cart.Where(o => o.VariableId == request.VariableId).FirstOrDefault();
                int index = cart.IndexOf(singleCartItem);
                singleCartItem.Qty += request.Qty;
                cart.Remove(singleCartItem);
                cart.Insert(index, singleCartItem);
            }
            else
            {
                var product = await (from pv in _db.Variants
                                     join vi in _db.VariantImages on pv.Id equals vi.VariantId into vilist
                                     from vi in vilist.DefaultIfEmpty()
                                     join i in _db.Galleries on vi.ImageId equals i.Id into ilist
                                     from i in ilist.DefaultIfEmpty()
                                     where pv.Id == request.VariableId
                                     select new CartDto
                                     {
                                         ProductId = pv.ProductId,
                                         VariableId = pv.Id,
                                         Title = pv.Title,
                                         Price = pv.Price,
                                         Qty = pv.Quantity,
                                         Sku = pv.Sku,
                                         Image = i.Name
                                     }).FirstOrDefaultAsync();

                CartDto cartVM = new CartDto();
                cartVM.ProductId = product.ProductId;
                cartVM.VariableId = product.VariableId;
                cartVM.Title = product.Title;
                cartVM.Price = product.Price;
                cartVM.Qty = request.Qty;
                cartVM.Image = product.Image;
                cartVM.Sku = product.Sku;
                cart.Add(cartVM);
            }

            _cookie.Set("shop-cart", JsonSerializer.Serialize(cart), 24 * 60);
            return Unit.Value;
        }
    }
}
