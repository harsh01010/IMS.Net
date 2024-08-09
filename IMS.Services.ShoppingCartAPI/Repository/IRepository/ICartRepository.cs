using IMS.Services.ShoppingCartAPI.Models.Dto;

namespace IMS.Services.ShoppingCartAPI.Repository.IRepository
{
    public interface ICartRepository
    {
        //public Task<ReturnCartDto> GetAsync(Guid id);

        public Task<String> UpsertAsync(Guid cartId,Guid productId,string token=null);
        public Task<String> DeleteProductFromCartAsync(Guid cartId,Guid productId,string token=null);

        public Task<ReturnCartDto> GetCartAsync(Guid cartId,string token=null);
    }
}
