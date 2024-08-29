using IMS.API.Repository.IRepository;
using IMS.API.Repository.IRepository.IProduct;
using IMS.API.Data;
using Dapper;
using IMS.API.Models.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using IMS.API.Models.Dto.Product;
using IMS.API.Models.Dto;
using System.Data;
namespace IMS.API.Repository.Implementations.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                /*
                string sqlQuery = @"SELECT * FROM Products p INNER JOIN
                                    Categories c on p.CategoryId = c.CategoryId";
                */
                var products = await connection.QueryAsync<ProductModel>("GetAllProducts",commandType:CommandType.StoredProcedure);
                return products.ToList();
            }
        }

        public async Task<ProductModel?> GetByIdAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
              /*   string sqlQuery = "SELECT * FROM Products p INNER JOIN Categories c ON p.CategoryId = c.CategoryId WHERE ProductId = @ProductId";*/
                var product = await connection.QuerySingleOrDefaultAsync<ProductModel>("GetProductById", new { ProductId = id },commandType:CommandType.StoredProcedure);
                return product;
            }
        }

        public async Task<ProductModel> CreateAsync(ProductModel product)
        {
            // Generate a new GUID for the ProductId
            product.ProductId = Guid.NewGuid();

            using (var connection = new SqlConnection(_connectionString))
            {
                /*
                // SQL query to insert the new product
                string sqlQuery = @"
            INSERT INTO Products (ProductId, Name, Description,AvailableQuantity, Price, CategoryId, ImageUrl, ImageLocalPath) 
            VALUES (@ProductId, @Name, @Description,@AvailableQuantity, @Price, @CategoryID, @ImageUrl, @ImageLocalPath);
             SELECT * FROM Products WHERE ProductId = @ProductId;";
                */

                var parameters = new DynamicParameters(new
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    AvailableQuantity = product.AvailableQuantity,
                    Price = product.Price,
                    CategoryID = product.CategoryId,
                    ImageUrl = product.ImageUrl,
                    ImageLocalPath = product.ImageLocalPath
                });

                // Execute the insert query and return the created product
                var createdProduct = await connection.QuerySingleOrDefaultAsync<ProductModel>("CreateProduct", parameters, commandType: CommandType.StoredProcedure);
                return createdProduct;
            }
        }


        public async Task<ProductModel> UpdateAsync(ProductModel product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                /*
                string sqlQuery = @"
            UPDATE Products SET 
                Name = @Name, 
                Description = @Description, 
                AvailableQuantity=@AvailableQuantity,
                Price = @Price,
                CategoryId = @CategoryId,
                ImageUrl = @ImageUrl,
                ImageLocalPath = @ImageLocalPath
            WHERE ProductId = @ProductId;";
                */

                var parameters = new DynamicParameters(new
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    AvailableQuantity = product.AvailableQuantity,
                    Price = product.Price,
                    CategoryID = product.CategoryId,
                    ImageUrl = product.ImageUrl,
                    ImageLocalPath = product.ImageLocalPath
                });


                // Execute the update query
                await connection.ExecuteAsync("UpdateProduct", parameters,commandType:CommandType.StoredProcedure);

                // Return the updated product
                return product;
            }
        }


        public async Task<ProductModel?> DeleteAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var product = await GetByIdAsync(id);
                if (product != null)
                {
                  /*  string sqlQuery = "DELETE FROM Products WHERE ProductId = @ProductId";*/
                    await connection.ExecuteAsync("DeleteProduct", new { ProductId = id },commandType:CommandType.StoredProcedure);
                }
                return product;
            }
        }


        public async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                /*var sqlQuery = "SELECT * FROM Categories";*/
                var categories = await connection.QueryAsync<CategoryModel>("GetCategories",commandType:CommandType.StoredProcedure);
                return categories.ToList();
            }

        }

        public async Task<List<ProductModel>> GetAllProductsByCategoryId(Guid categoryId)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                /*var sqlQuery = @"SELECT * FROM Products p INNER JOIN 
                                Categories c ON p.CategoryId = c.CategoryId
                                 WHERE p.CategoryID = @categoryID";*/

                var products = await connection.QueryAsync<ProductModel>("GetAllProductsByCategoryId", new { categoryId = categoryId },commandType:CommandType.StoredProcedure);

                return products.ToList();
            }

        }
        public async Task<bool> AddNewCategoryAsync(AddCategoryDto category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                /*var sqlQuery = "INSERT INTO Categories(CategoryId,CategoryName) VALUES(@CId,@CName)";*/
                var rowsAffected = await connection.ExecuteAsync("AddNewCategory", new { CId = Guid.NewGuid(), CName = category.CategoryName },commandType:CommandType.StoredProcedure);
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                    return false;
            }

        }

        public async Task<List<ProductModel>> GetProductPageAsync(GetPageRequestDto getPageRequest)
        {

            using(var connection = new SqlConnection(_connectionString))
            {
                /*var sqlQuery = @"SELECT p.*,c.CategoryName FROM Products p INNER JOIN Categories c ON p.CategoryId=c.CategoryId
                                ORDER BY p.Name ASC OFFSET @skip ROWS FETCH NEXT @pageSize ROWS ONLY";*/

                var products = await connection.QueryAsync<ProductModel>("GetProductPage", new {skip = (getPageRequest.PageNum-1)*getPageRequest.PageSize,
                                                                          pageSize = getPageRequest.PageSize}, commandType: CommandType.StoredProcedure);

                return products.ToList();

            }
          
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {

            using(var connection  = new SqlConnection(_connectionString))
            {
                //var sqlQuery = @"DELETE FROM Categories WHERE CategoryId = @id";

                var rowsAffected = await connection.ExecuteAsync("DeleteCategory", new { id = id }, commandType: CommandType.StoredProcedure);

                if (rowsAffected > 0)
                    return true;
                else return false;
            }
          
        }
    }
}
