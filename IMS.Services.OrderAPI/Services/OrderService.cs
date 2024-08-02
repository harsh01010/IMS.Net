using IMS.Services.OrderAPI.Models;
using IMS.Services.OrderAPI.Repositiory;
using IMS.Services.ShoppingCartAPI.Repository.IRepository;

namespace IMS.Services.OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public async Task<Order> PlaceOrderAsync(Guid userId)
        {
            var cartItems = await _cartRepository.GetCartAsync(userId);
            var order = await _orderRepository.CreateOrderAsync(userId, cartItems);
            //await _cartRepository.ClearCartAsync(userId);
            return order;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            return await _orderRepository.GetOrderAsync(orderId);
        }

        public async Task<List<Order>> GetOrderHistoryAsync(int userId)
        {
            return await _orderRepository.GetOrderHistoryAsync(userId);
        }
    }
}
