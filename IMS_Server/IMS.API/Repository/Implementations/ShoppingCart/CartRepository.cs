using IMS.API.Models.Dto;
using IMS.API.Models.Dto.ShoppingCart;
using IMS.API.Repository.IRepository.IAuth;
using IMS.API.Repository.IRepository.IProduct;
using IMS.API.Repository.IRepository.IShoppingCart;
using IMS.API.Models.Domain.ShoppingCart;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Text;
using IMS.API.Models.Domain.Product;

namespace IMS.API.Repository.Implementations.ShoppingCart
{
    public class CartRepository : ICartRepository
    {
        private readonly IEmailSender emailSender;
        private readonly IAuthRepository authRepository;
        private readonly string connectionString;

        public CartRepository(IEmailSender emailSender, IAuthRepository authRepository, IConfiguration configuration)
        {
            this.emailSender = emailSender;
            this.authRepository = authRepository;
            connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }

        public async Task<string> UpsertAsync(Guid CartId, Guid productId, string token = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var fetchProductQuery = @"SELECT * FROM Products p INNER JOIN 
                                          Categories c ON p.CategoryID = c.CategoryId WHERE ProductId = @Id";
                var product = await connection.QueryFirstOrDefaultAsync<ProductModel>(fetchProductQuery, new { Id = productId });

                if (product == null)
                    return ""; // Product not found

                var fetchIfCartExistQuery = "SELECT * FROM Carts WHERE Id = @Id";
                var cart = await connection.QueryFirstOrDefaultAsync<CartModel>(fetchIfCartExistQuery, new { Id = CartId });

                if (cart != null)
                {
                    var updateExistingCartQuery = @"
                        UPDATE Carts 
                        SET TotalProductQty = TotalProductQty + 1, 
                            TotalValue = TotalValue + @Price 
                        WHERE Id = @Id";
                    await connection.ExecuteAsync(updateExistingCartQuery, new { Id = cart.Id, Price = product.Price });
                }
                else
                {
                    var insertIntoCartQuery = @"
                        INSERT INTO Carts (Id, TotalProductQty, TotalValue) 
                        VALUES (@Id, @TotalProductQty, @TotalValue)";
                    await connection.ExecuteAsync(insertIntoCartQuery, new { Id = CartId, TotalProductQty = 1, TotalValue = product.Price });
                }

                // Insert or update CartProducts table
                var fetchCartProductQuery = "SELECT * FROM CartProducts WHERE CartId = @CartId AND ProductId = @ProductId";
                var cartProduct = await connection.QueryFirstOrDefaultAsync<CartProductModel>(fetchCartProductQuery, new { CartId, ProductId = productId });

                if (cartProduct != null)
                {
                    var updateCartProductQuery = @"
                        UPDATE CartProducts 
                        SET ProductCount = ProductCount + 1 
                        WHERE CartId = @CartId AND ProductId = @ProductId";
                    await connection.ExecuteAsync(updateCartProductQuery, new { CartId, ProductId = productId });
                }
                else
                {
                    var insertCartProductQuery = @"
                        INSERT INTO CartProducts (CartId, ProductId, ProductCount) 
                        VALUES (@CartId, @ProductId, @ProductCount)";
                    await connection.ExecuteAsync(insertCartProductQuery, new {Id=Guid.NewGuid(), CartId, ProductId = productId, ProductCount = 1 });
                }

                return $"Added {product.Name} to the Cart";
            }
        }

        public async Task<string> DeleteProductFromCartAsync(Guid cartId, Guid productId, string token = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var product = await connection.QueryFirstOrDefaultAsync<ProductModel>("SELECT * FROM Products WHERE ProductId = @Id", new { Id = productId });

                if (product == null)
                    return "";

                var cartProduct = await connection.QueryFirstOrDefaultAsync<CartProductModel>("SELECT * FROM CartProducts WHERE CartId = @CartId AND ProductId = @ProductId", new { CartId = cartId, ProductId = productId });

                if (cartProduct == null)
                    return "";

                var updateCartQuery = @"
                    UPDATE Carts 
                    SET TotalProductQty = TotalProductQty - @ProductCount, 
                        TotalValue = TotalValue - (@Price * @ProductCount) 
                    WHERE Id = @CartId";
                await connection.ExecuteAsync(updateCartQuery, new { CartId = cartId, ProductCount = cartProduct.ProductCount, Price = product.Price });

                var deleteCartProductQuery = "DELETE FROM CartProducts WHERE CartId = @CId AND ProductId = @PId";
                await connection.ExecuteAsync(deleteCartProductQuery, new { CId=cartId,PId = productId });

                return $"{product.Name} has been deleted from the Cart";
            }
        }

        public async Task<ReturnCartDto> GetCartAsync(Guid cartId, string token = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var cart = await connection.QueryFirstOrDefaultAsync<CartModel>("SELECT * FROM Carts WHERE Id = @Id", new { Id = cartId });

                if (cart == null)
                    return new ReturnCartDto();

                var cartProductsQuery = @"
                                 SELECT p.Name, p.ProductId, p.Price, c.CategoryName, cp.ProductCount 
                                 FROM CartProducts cp 
                                 JOIN Products p ON cp.ProductId = p.ProductId 
                                 JOIN Categories c ON p.CategoryId = c.CategoryId 
                                 WHERE cp.CartId = @CartId";

                var products = await connection.QueryAsync<ReturnProductFromCartDto>(cartProductsQuery, new { CartId = cartId });

                return new ReturnCartDto
                {
                    Id = cartId,
                    TotalValue = cart.TotalValue,
                    TotalProductQty = cart.TotalProductQty,
                    Products = products.ToList()
                };
            }
        }

        public async Task<string> SendCartByEmailAsync(ReturnCartDto cart, string token = null)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<h1>Shopping Cart Details</h1>");
            sb.AppendLine("<p><strong>Cart ID:</strong> " + cart.Id + "</p>");
            sb.AppendLine("<p><strong>Total Quantity:</strong> " + cart.TotalProductQty + "</p>");
            sb.AppendLine("<p><strong>Total Value:</strong> " + string.Format("{0:C}", cart.TotalValue) + "</p>");
            sb.AppendLine("<hr/>");

            sb.AppendLine("<h2>Product Details</h2>");
            sb.AppendLine("<table border='1' style='width:100%; border-collapse:collapse;'>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th style='padding:8px; text-align:left;'>Product Name</th>");
            sb.AppendLine("<th style='padding:8px; text-align:left;'>Category</th>");
            sb.AppendLine("<th style='padding:8px; text-align:right;'>Price</th>");
            sb.AppendLine("<th style='padding:8px; text-align:right;'>Quantity</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            foreach (var product in cart.Products)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine("<td style='padding:8px;'>" + product.Name + "</td>");
                sb.AppendLine("<td style='padding:8px;'>" + product.CategoryName + "</td>");
                sb.AppendLine("<td style='padding:8px; text-align:right;'>" + string.Format("{0:C}", product.Price) + "</td>");
                sb.AppendLine("<td style='padding:8px; text-align:right;'>" + product.ProductCount + "</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");

            var emailString = sb.ToString();
            var subject = $"Cart :{cart.Id}";

            var user = await authRepository.GetById(cart.Id);

            if (user != null)
            {
                var res = await emailSender.EmailSendAsync(new SendEmailRequestDto { Email = user.Email, Subject = subject, Body = emailString });
                if (res)
                {
                    return "Sent Successfully";
                }
            }

            return "";
        }

        public async Task<bool> DeleteCartAsync(Guid cartId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var deleteCartQuery = "DELETE FROM Carts WHERE Id = @Id";
                var rowsAffected = await connection.ExecuteAsync(deleteCartQuery, new { Id = cartId });

                return rowsAffected > 0;
            }
        }
    }
}
