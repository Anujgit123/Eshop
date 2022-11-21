using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Constants;
using Ecommerce.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore.DynamicLinq;

namespace Ecommerce.Application.Handlers.Orders.Commands
{
    public class CreateOrderCheckoutCommand : IRequest<Response<string>>
    {
        public CheckoutDto Checkout { get; set; }
    }
    public class CreateOrderCheckoutCommandHandler : IRequestHandler<CreateOrderCheckoutCommand, Response<string>>
    {
        private readonly IDataContext _db;
        private readonly ICookieService _cookie;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IConfiguration _configuration;
        public CreateOrderCheckoutCommandHandler(IDataContext db, IMapper mapper, ICookieService cookie, ICurrentUser currentUser, IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _cookie = cookie;
            _currentUser = currentUser;
            _configuration = configuration;
        }

        public async Task<Response<string>> Handle(CreateOrderCheckoutCommand request, CancellationToken cancellationToken)
        {
            var status = false;
            var orderId = 0;
            string secretKey = _configuration.GetValue<string>("Stripe:SecretKey");
            string currency = _configuration.GetValue<string>("Stripe:Currency");

            List<CartDto> cart = new();
            if (_cookie.Contains("shop-cart")) cart = JsonSerializer.Deserialize<List<CartDto>>(_cookie.Get("shop-cart"));
            if (cart == null || !cart.Any()) return Response<string>.Fail("Your Cart is Empty!");

            var deliveryCharge = _db.DeliveryMethods.Where(o => o.Id == request.Checkout.DeliveryMethod).Select(o => o.Price).FirstOrDefault();

            using var transaction = _db.BeginTransaction();
            try
            {
                OrderPayment payment = new();
                var order = new Order()
                {
                    UserId = _currentUser.UserName,
                    CustomerAddress = request.Checkout.CustomerAddress,
                    CustomerComment = request.Checkout.CustomerComment,
                    CustomerEmail = request.Checkout.CustomerEmail,
                    //CustomerCity = request.Checkout.CustomerCity,
                    CustomerMobile = request.Checkout.CustomerMobile,
                    CustomerName = request.Checkout.CustomerName,
                    DeliveryMethodId = request.Checkout.DeliveryMethod,
                    //ShippingAreaId = request.Checkout.ShippingAreaId,
                    PaymentMethod = request.Checkout.PaymentMethod,
                    DeliveryCharge = deliveryCharge,
                    OrderAmount = cart.Select(o => o.Price * o.Qty).Sum()
                };
                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();

                orderId = order.Id;

                foreach (var item in cart)
                {
                    var deductStock = await _db.Variants.FindAsync(item.VariableId);
                    if (deductStock.Quantity < item.Qty) return Response<string>.Fail($"Sorry, [{item.Title}] Doesn't Have Enough Stock for Order. Please Reduce the Stock & Try Again.");
                    deductStock.Quantity -= item.Qty;
                    _db.Variants.Update(deductStock);

                    OrderDetails orderDetails = new OrderDetails()
                    {
                        ProductVariantId = item.VariableId,
                        ProductName = item.Title,
                        UnitPrice = item.Price,
                        Qty = item.Qty,
                        OrderId = orderId
                    };
                    await _db.OrderDetails.AddAsync(orderDetails);


                }

                if (request.Checkout.PaymentMethod == PaymentMethod.Paypal)
                {
                    if (request.Checkout?.PaypalModel?.TransactionID == null) return Response<string>.Fail("Please Provide Valid Payment Info!");

                    payment.Reference = request.Checkout.PaypalModel.TransactionID;
                    payment.PaymentType = request.Checkout.PaymentMethod;
                    payment.Amount = (decimal)(order.OrderAmount + order.DeliveryCharge);
                    payment.OrderId = orderId;
                    await _db.OrderPayments.AddAsync(payment);

                    order.PaymentStatus = PaymentStatus.Paid.ToString();
                    _db.Orders.Update(order);

                    var paymentReceivedStatus = _db.OrderStatusValues.FirstOrDefault(o => o.StatusValue == DefaultOrderStatusValue.PaymentReceived().StatusValue);
                    OrderStatus orderStatus2 = new OrderStatus()
                    {
                        OrderId = orderId,
                        OrderStatusValueId = paymentReceivedStatus?.Id,
                        Description = paymentReceivedStatus?.Description
                    };
                    await _db.OrderStatus.AddAsync(orderStatus2);
                }


                if (request.Checkout.PaymentMethod == PaymentMethod.CardPayment)
                {
                    if (request.Checkout?.StripeModel?.Token == null) return Response<string>.Fail("Please Provide Valid Card Info!");
                    //Stripe.StripeConfiguration.SetApiKey("your Publishable key");
                    //Stripe.StripeConfiguration.ApiKey = "your Secret key";

                    //var myCharge = new Stripe.ChargeCreateOptions();
                    //// always set these properties
                    //myCharge.Amount = 500;
                    //myCharge.Currency = "USD";
                    //myCharge.ReceiptEmail = request.Checkout.CustomerEmail;
                    //myCharge.Description = "Sample Charge";
                    //myCharge.Source = stripeToken;
                    //myCharge.Capture = true;
                    //var chargeService = new Stripe.ChargeService();
                    //Stripe.Charge stripeCharge = chargeService.Create(myCharge);


                    Stripe.StripeConfiguration.ApiKey = secretKey;
                    var options = new Stripe.ChargeCreateOptions
                    {
                        Amount = Decimal.ToInt32(order.OrderAmount + deliveryCharge) * 100,
                        Currency = currency,
                        Description = "Order ID: " + order.InvoiceNo,
                        Source = request.Checkout.StripeModel.Token,
                    };
                    var service = new Stripe.ChargeService();
                    var charge = await service.CreateAsync(options);
                    var res = charge.Id;

                    //StripePaymentDto stripePayment = new StripePaymentDto()
                    //{
                    //    CustomerName = request.Checkout.CustomerName,
                    //    Address = request.Checkout.CustomerAddress,
                    //    Amount = Convert.ToInt32(cart.Select(o => o.Price * o.Qty).Sum() + deliveryCharge),
                    //    CardNumder = "4242424242424242",
                    //    CVC = "222",
                    //    Email = request.Checkout.CustomerEmail,
                    //    Month = 12,
                    //    Year = 2023
                    //};

                    //var result = await StripePayment.PayAsync(stripePayment);
                    if (charge.Status.ToLower() == "succeeded")
                    {

                        payment.Reference = charge.Id;
                        payment.PaymentType = request.Checkout.PaymentMethod;
                        payment.Amount = (decimal)(order.OrderAmount + order.DeliveryCharge);
                        payment.OrderId = orderId;
                        await _db.OrderPayments.AddAsync(payment);

                        order.PaymentStatus = PaymentStatus.Paid.ToString();
                        _db.Orders.Update(order);
                        var paymentReceivedStatus = _db.OrderStatusValues.FirstOrDefault(o => o.StatusValue == DefaultOrderStatusValue.PaymentReceived().StatusValue);
                        var orderStatus2 = new OrderStatus()
                        {
                            OrderId = orderId,
                            OrderStatusValueId = paymentReceivedStatus?.Id,
                            Description = paymentReceivedStatus?.Description
                        };
                        await _db.OrderStatus.AddAsync(orderStatus2);
                    }
                }

                var pendingStatus = _db.OrderStatusValues.FirstOrDefault(o => o.StatusValue == DefaultOrderStatusValue.Pending().StatusValue);
                var orderStatus = new OrderStatus()
                {
                    OrderId = orderId,
                    OrderStatusValueId = pendingStatus?.Id,
                    Description = pendingStatus?.Description
                };
                await _db.OrderStatus.AddAsync(orderStatus);

                if (request.Checkout.WillSaveInfo == true)
                {
                    var customer = _db.Customers.FirstOrDefault(o => o.ApplicationUserId == _currentUser.UserId);

                    if (customer != null)
                    {
                        customer.FullName = request.Checkout?.CustomerName;
                        customer.ShippingAddress = request.Checkout?.CustomerAddress;
                        customer.Email = request.Checkout?.CustomerEmail;
                        customer.Phone = request.Checkout?.CustomerMobile;
                        _db.Customers.Update(customer);
                    }
                    else
                    {
                        var newCustomer = new Customer
                        {
                            FullName = request.Checkout?.CustomerName,
                            ShippingAddress = request.Checkout?.CustomerAddress,
                            Email = request.Checkout?.CustomerEmail,
                            Phone = request.Checkout?.CustomerMobile,
                            ApplicationUserId = _currentUser.UserId
                        };
                        _db.Customers.Add(newCustomer);
                    }
                }

                await _db.SaveChangesAsync();
                transaction.Commit();
                status = true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                status = false;
            }

            if (status)
            {
                _cookie.Remove("shop-cart");
                return Response<string>.Success(orderId.ToString());
            }
            return Response<string>.Fail("Failed to add item!");

        }
    }
}
