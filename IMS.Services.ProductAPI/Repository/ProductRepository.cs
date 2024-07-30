using IMS.Services.ProductAPI.Data;
using IMS.Services.ProductAPI.Models;
using IMS.Services.ProductAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace IMS.Services.ProductAPI.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext db;
		public ProductRepository(AppDbContext db)
		{
			this.db = db;
		}

		public async Task<List<Product>> GetAllAsync()
		{
			return await db.Products.ToListAsync();
		}

		public async Task<Product?> GetByIdAsync(Guid id)
		{
			return await db.Products.FindAsync(id);
		}

		public async Task<Product> CreateAsync(Product Product)
		{
			await db.Products.AddAsync(Product);
			await db.SaveChangesAsync();
			return Product;
		}

		public async Task<Product> UpdateAsync(Product Product)
		{
			db.Products.Update(Product);
			await db.SaveChangesAsync();
			return Product;
		}

		public async Task<Product?> DeleteAsync(Guid id)
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
