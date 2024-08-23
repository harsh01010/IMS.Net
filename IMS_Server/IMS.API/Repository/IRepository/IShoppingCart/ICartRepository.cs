using IMS.API.Models.Dto.ShoppingCart;

namespace IMS.API.Repository.IRepository.IShoppingCart
{
    public interface ICartRepository
    {
        public Task<String> UpsertAsync(Guid cartId, Guid productId, string token = null);
        public Task<String> DeleteProductFromCartAsync(Guid cartId, Guid productId, string token = null);

        public Task<ReturnCartDto> GetCartAsync(Guid cartId, string token = null);

        public Task<bool> DeleteCartAsync(Guid cartId);
        public Task<string> SendCartByEmailAsync(ReturnCartDto cartDto, string token = null);
    }
}
