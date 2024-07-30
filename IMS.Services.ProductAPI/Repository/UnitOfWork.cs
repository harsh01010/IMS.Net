using IMS.Services.ProductAPI.Data;
using IMS.Services.ProductAPI.Repository.IRepository;

namespace IMS.Services.ProductAPI.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext db;
		public IProductRepository Product { get; private set; }

		public UnitOfWork(AppDbContext db)
		{
			this.db = db;
			Product = new ProductRepository(db);
		}
	}
}
