using IMS.API.Models.Dto.ShippingAddress;

namespace IMS.API.Repository.IRepository.IShippingAddress
{
    public interface IShippingAddressRepository
    {
        public Task<string> AddAddressAsync(Guid userId, AddAddressRequestDto addAddressRequestDto);

        public Task<string> DeleteAddressAsync(Guid shippingAddressId);

        public Task<ReturnShippingAddressDto> GetShippingAddressAsync(Guid shippingAddressId);
        public Task<List<ReturnShippingAddressDto>> GetAllAddressAsync(Guid userId);
    }
}
