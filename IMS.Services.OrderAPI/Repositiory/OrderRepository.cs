using IMS.Services.OrderAPI.Models;
using IMS.Services.ShoppingCartAPI.Models.Domain;

namespace IMS.Services.OrderAPI.Repositiory
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Guid userId, List<Cart> cartItems)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                TotalAmount = cartItems.
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Product.Price
                };
                _context.OrderItems.Add(orderItem);
            }

            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<List<Order>> GetOrderHistoryAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }
    }
}
