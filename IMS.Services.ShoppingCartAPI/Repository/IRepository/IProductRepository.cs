

using IMS.Services.ShoppingCartAPI.Models.Dto;

namespace IMS.Services.ShoppingCartAPI.Repository.IRepository
{
    public interface IProductRepository
    {
        public Task<ProductResponseDto> GetProductById(Guid id);
    }
}
