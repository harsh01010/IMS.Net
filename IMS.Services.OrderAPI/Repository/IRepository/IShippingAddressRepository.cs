using IMS.Services.OrderAPI.Models.DTO;

namespace IMS.Services.OrderAPI.Repository.IRepository
{
    public interface IShippingAddressRepository
    {
        public Task<string> AddAddressAsync(Guid userId, AddAddressRequestDto addAddressRequestDto);

        public Task<string> DeleteAddressAsync(Guid shippingAddressId);

        public Task<List<ReturnShippingAddressDto>> GetAllAddressAsync(Guid userId);
    }
}
