using IMS.Services.ProductAPI.Models;

namespace IMS.Services.ProductAPI.Repository.IRepository
{
	public interface IProductRepository
	{
		Task<List<Product>> GetAllAsync();
		Task<Product?> GetByIdAsync(Guid id);
		Task<Product> CreateAsync(Product Product);
		Task<Product> UpdateAsync(Product Product);
		Task<Product?> DeleteAsync(Guid id);
	}
}
