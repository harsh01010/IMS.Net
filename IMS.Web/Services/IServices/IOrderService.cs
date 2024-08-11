using IMS.Web.Models;
using IMS.Web.Models.Order;

namespace IMS.Web.Services.IServices
{
    public interface IOrderService
    {
        public Task<ResponseDto> AddAddressAsync(Guid userId, AddAddressRequestDto addAddressRequestDto);

        public Task<ResponseDto> DeleteAddressAsync(Guid shippingAddressId);

        public Task<ResponseDto> GetAllAddressAsync(Guid userId);

        public Task<ResponseDto> ConfirmAsync(Guid cartId, PlaceOrderRequestDto placeOrderRequestDto);

        public Task<ResponseDto> GetAllOrdersAsync();
    }
}
