using AutoMapper;
using IMS.Services.OrderAPI.Data;
using IMS.Services.OrderAPI.Models.Domain;
using IMS.Services.OrderAPI.Models.DTO;
using IMS.Services.OrderAPI.Repository.IRepository;
using IMS.Services.OrderAPI.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace IMS.Services.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ICartService cartService;
        private readonly OrderDbContext orderDb;
        private readonly ShippingAddressDbContext shippingAddressDb;
        private readonly IAuthService authService;
        private readonly IMapper mapper;

        public OrderRepository(ICartService cartService,OrderDbContext orderDb,ShippingAddressDbContext shippingAddressDb,IAuthService authService,IMapper mapper
            )
        {
            this.cartService = cartService;
            this.orderDb = orderDb;
            this.shippingAddressDb = shippingAddressDb;
            this.authService = authService;
            this.mapper = mapper;
        }

   

        public async Task<string> PlaceOrderAsync(Guid cartId, Guid shippingAddressId,string token=null)
        {
            try
            {

                //fetch the cart
                var cart = await cartService.GetCartById(cartId,token);

                //fetch the address
                var shippingAddress = await shippingAddressDb.ShippingAddresses.FirstOrDefaultAsync(x => x.shippingAddressId == shippingAddressId);

                //payment
                bool payment = true;

                //update the orders table
                var order = new Order { CustomerId = cartId, OrderTime = DateTime.Now, OrderValue = cart.TotalValue };
                if (cart.TotalValue != 0 && shippingAddress != null && payment)
                {
                    order.Status = true;
                }
                else
                {
                    order.Status = false;
                }
                await orderDb.Orders.AddAsync(order);


                //update the orderitem table
                var products = cart.Products;

                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        var orderItem = new OrderItem { OrderId = order.OrderId, ProductId = product.ProductId, ProductCount = product.ProductCount };
                        await orderDb.OrderItems.AddAsync(orderItem);
                    }
                }
                await orderDb.SaveChangesAsync();

                //make the cart empty


                //send confirmation to user via emial
                var user = await authService.GetUserByIdAsync(cartId,token);
                if(user.IsSuccess)
                {
                    var emailBody = $"Congratulations {user.Result.Name} , your order with amount {cart.TotalValue} has been placed!";
                    var emailSub = "Order Confirmation";
                    var res = await authService.SendMailAsync(new SendMailRequestDto { Email = user.Result.Email, Subject = emailSub, Body = emailBody },token);
                   
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
            var orders = await orderDb.Orders.ToListAsync();

            if(orders.Any())
            {
                return mapper.Map<List<OrderDetailsDto>>(orders);
            }
            return new List<OrderDetailsDto>();
        }

    }
}
