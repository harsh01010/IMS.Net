using IMS.API.Models.Domain.Product;
using IMS.API.Models.Dto;
using IMS.API.Models.Dto.Product;

namespace IMS.API.Repository.IRepository.IProduct
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel?> GetByIdAsync(Guid id);
        Task<ProductModel> CreateAsync(ProductModel Product);
        Task<ProductModel> UpdateAsync(ProductModel Product);
        Task<ProductModel?> DeleteAsync(Guid id);
        Task<List<ProductModel>> GetProductPageAsync(GetPageRequestDto getPageRequest);


        Task<List<CategoryModel>> GetAllCategoriesAsync();
        Task<List<ProductModel>>GetAllProductsByCategoryId(Guid categoryId);
        Task<bool> AddNewCategoryAsync(AddCategoryDto category);

        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
