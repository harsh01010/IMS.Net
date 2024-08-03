using IMS.Services.OrderAPI.Models;
using IMS.Services.ShoppingCartAPI.Models.Domain;

namespace IMS.Services.OrderAPI.Repositiory
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(int userId, List<Cart> cartItems);
        Task<Order> GetOrderAsync(int orderId);
        Task<List<Order>> GetOrderHistoryAsync(int userId);
    }
}
