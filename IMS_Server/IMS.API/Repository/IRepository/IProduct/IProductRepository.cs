using IMS.API.Models.Domain.Product;

namespace IMS.API.Repository.IRepository.IProduct
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel?> GetByIdAsync(Guid id);
        Task<ProductModel> CreateAsync(ProductModel Product);
        Task<ProductModel> UpdateAsync(ProductModel Product);
        Task<ProductModel?> DeleteAsync(Guid id);
    }
}
