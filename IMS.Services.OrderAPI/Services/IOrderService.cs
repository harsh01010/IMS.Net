using IMS.Services.OrderAPI.Models;

namespace IMS.Services.OrderAPI.Services
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(int userId);
        Task<Order> GetOrderAsync(int orderId);
        Task<List<Order>> GetOrderHistoryAsync(int userId);
    }
}
