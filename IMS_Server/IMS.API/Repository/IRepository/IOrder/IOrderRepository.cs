using IMS.API.Models.Dto.Order;

namespace IMS.API.Repository.IRepository.IOrder
{
    public interface IOrderRepository
    {
        public Task<string> PlaceOrderAsync(Guid cartId, Guid shippingAddressId, string token = null);

        public Task<List<OrderDetailsDto>> GetAllOrdersAsync();

        public Task<List<OrderDto>> GetOrderHistory(Guid customerId);
    }
}
