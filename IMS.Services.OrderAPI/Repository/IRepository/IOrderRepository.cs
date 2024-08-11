using IMS.Services.OrderAPI.Models.DTO;

namespace IMS.Services.OrderAPI.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<string> PlaceOrderAsync(Guid cartId, Guid shippingAddressId,string token=null);

        public Task<List<OrderDetailsDto>> GetAllOrdersAsync();
    }
}
