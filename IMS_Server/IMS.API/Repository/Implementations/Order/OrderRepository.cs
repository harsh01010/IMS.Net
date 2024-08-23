using AutoMapper;
using IMS.API.Data;
using IMS.API.Models.Domain.Order;
using IMS.API.Models.Dto;
using IMS.API.Models.Dto.Order;
using IMS.API.Repository.IRepository.IAuth;
using IMS.API.Repository.IRepository.IOrder;
using IMS.API.Repository.IRepository.IShippingAddress;
using IMS.API.Repository.IRepository.IShoppingCart;
using Microsoft.EntityFrameworkCore;

namespace IMS.API.Repository.Implementations.Order
{
    public class OrderRepository:IOrderRepository
    {

       
        private readonly IMapper mapper;
        private readonly IMSDbContext db;
        private readonly ICartRepository cartRepository;
        private readonly IShippingAddressRepository shippingAddressRepository;
        private readonly IAuthRepository authRepository;
        private readonly IEmailSender emailSender;

        public OrderRepository( IMapper mapper,IMSDbContext db,ICartRepository cartRepository,IShippingAddressRepository shippingAddressRepository,IAuthRepository authRepository,IEmailSender emailSender)
        {
            this.mapper = mapper;
            this.db = db;
            this.cartRepository = cartRepository;
            this.shippingAddressRepository = shippingAddressRepository;
            this.authRepository = authRepository;
            this.emailSender = emailSender;
        }



        public async Task<string> PlaceOrderAsync(Guid cartId, Guid shippingAddressId, string token = null)
        {
            try
            {

                //fetch the cart
                var cart = await cartRepository.GetCartAsync(cartId);

                //fetch the address
                var shippingAddress = await shippingAddressRepository.GetShippingAddressAsync(shippingAddressId);

                //payment
                bool payment = true;

                //update the orders table
                var order = new OrderModel { CustomerId = cartId, OrderTime = DateTime.Now, OrderValue = cart.TotalValue };
                if (cart.TotalValue != 0 && shippingAddress != null && payment)
                {
                    order.Status = true;
                }
                else
                {
                    order.Status = false;
                }
                await db.Orders.AddAsync(order);


                //update the orderitem table
                var products = cart.Products;

                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        var orderItem = new OrderItemModel { OrderId = order.OrderId, ProductId = product.ProductId, ProductCount = product.ProductCount };
                        await db.OrderItems.AddAsync(orderItem);
                    }
                }
                await db.SaveChangesAsync();

                //make the cart empty


                //send confirmation to user via emial
                var user = await authRepository.GetById(cartId);
                if (user != null)
                {
                    var emailBody = $"Congratulations {user.Name} , your order with amount {cart.TotalValue} has been placed!";
                    var emailSub = "Order Confirmation";
                    var res = await emailSender.EmailSendAsync(new SendEmailRequestDto { Email = user.Email, Subject = emailSub, Body = emailBody });

                }


                var statusMessage = order.Status ? "placed Sucessfully" : "failed";


                return $"Order with id {order.OrderId} and value {order.OrderValue}, is  {statusMessage}";
            }
            catch (Exception ex)
            {
            }
            return "";
        }
        public async Task<List<OrderDetailsDto>> GetAllOrdersAsync()
        {
            var orders = await db.Orders.ToListAsync();

            if (orders.Any())
            {
                return mapper.Map<List<OrderDetailsDto>>(orders);
            }
            return new List<OrderDetailsDto>();
        }

    }
}
