using IMS.API.Repository.IRepository;
using IMS.API.Repository.IRepository.IProduct;
using IMS.API.Data;
using IMS.API.Models.Domain.Product;
using Microsoft.EntityFrameworkCore;
namespace IMS.API.Repository.Implementations.Product
{
    public class ProductRepository:IProductRepository
    {
        private readonly IMSDbContext db;
		public ProductRepository(IMSDbContext db)
		{
			this.db = db;
		}

		public async Task<List<ProductModel>> GetAllAsync()
		{
			return await db.Products.ToListAsync();
		}

		public async Task<ProductModel?> GetByIdAsync(Guid id)
		{
			return await db.Products.FindAsync(id);
		}

		public async Task<ProductModel> CreateAsync(ProductModel Product)
		{
			await db.Products.AddAsync(Product);
			await db.SaveChangesAsync();
			return Product;
		}

		public async Task<ProductModel> UpdateAsync(ProductModel Product)
		{
			db.Products.Update(Product);
			await db.SaveChangesAsync();
			return Product;
		}

		public async Task<ProductModel?> DeleteAsync(Guid id)
		{
			var Product = await db.Products.FindAsync(id);
			if (Product != null)
			{
				db.Products.Remove(Product);
				await db.SaveChangesAsync();
			}
			return Product;
		}
    }
}
