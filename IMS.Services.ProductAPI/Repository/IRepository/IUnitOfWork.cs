namespace IMS.Services.ProductAPI.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IProductRepository Product { get; }
	}
}
