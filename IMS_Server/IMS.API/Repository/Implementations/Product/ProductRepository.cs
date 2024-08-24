using IMS.API.Repository.IRepository;
using IMS.API.Repository.IRepository.IProduct;
using IMS.API.Data;
using Dapper;
using IMS.API.Models.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
namespace IMS.API.Repository.Implementations.Product
{
    public class ProductRepository:IProductRepository
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
                string sqlQuery = "SELECT * FROM Products";
                var products = await connection.QueryAsync<ProductModel>(sqlQuery);
                return products.ToList();
            }
        }

        public async Task<ProductModel?> GetByIdAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT * FROM Products WHERE ProductId = @ProductId";
                var product = await connection.QuerySingleOrDefaultAsync<ProductModel>(sqlQuery, new { ProductId = id });
                return product;
            }
        }

        public async Task<ProductModel> CreateAsync(ProductModel product)
        {
            // Generate a new GUID for the ProductId
            product.ProductId = Guid.NewGuid();

            using (var connection = new SqlConnection(_connectionString))
            {
                // SQL query to insert the new product
                string sqlQuery = @"
            INSERT INTO Products (ProductId, Name, Description, Price, CategoryName, ImageUrl, ImageLocalPath) 
            VALUES (@ProductId, @Name, @Description, @Price, @CategoryName, @ImageUrl, @ImageLocalPath);
            
       
            SELECT * FROM Products WHERE ProductId = @ProductId;";

                // Execute the insert query and return the created product
                var createdProduct = await connection.QuerySingleOrDefaultAsync<ProductModel>(sqlQuery, product);
                return createdProduct;
            }
        }


        public async Task<ProductModel> UpdateAsync(ProductModel product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"
            UPDATE Products SET 
                Name = @Name, 
                Description = @Description, 
                Price = @Price,
                CategoryName = @CategoryName,
                ImageUrl = @ImageUrl,
                ImageLocalPath = @ImageLocalPath
            WHERE ProductId = @ProductId;";

                // Execute the update query
                await connection.ExecuteAsync(sqlQuery, product);

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
                    string sqlQuery = "DELETE FROM Products WHERE ProductId = @ProductId";
                    await connection.ExecuteAsync(sqlQuery, new { ProductId = id });
                }
                return product;
            }
        }
    }
}
