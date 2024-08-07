using IMS.Services.OrderAPI.Data;
using IMS.Services.OrderAPI.Models.Domain;
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

        public OrderRepository(ICartService cartService,OrderDbContext orderDb,ShippingAddressDbContext shippingAddressDb)
        {
            this.cartService = cartService;
            this.orderDb = orderDb;
            this.shippingAddressDb = shippingAddressDb;
        }
        public async Task<string> PlaceOrderAsync(Guid cartId, Guid shippingAddressId)
        {
            try
            {

                //fetch the cart
                var cart = await cartService.GetCartById(cartId);

                //fetch the address
                var shippingAddress = await shippingAddressDb.ShippingAddresses.FirstOrDefaultAsync(x => x.shippingAddressId == shippingAddressId);

                //payment
                bool payment = true;

                //update the orders table
                var order = new Order { CustomerId = cartId, OrderTime = DateTime.Now, OrderValue = cart.TotalValue };
                if (cart.TotalValue == 0 && shippingAddress != null && payment)
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
               
                var statusMessage = order.Status ? "placed Sucessfully" : "failed";
                return $"Order with id {order.OrderId} and value {order.OrderValue}, is  {statusMessage}";
            }
            catch (Exception ex)
            {
            }
            return "";
        }
    }
}
