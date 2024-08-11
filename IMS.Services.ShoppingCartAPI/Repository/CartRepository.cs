using Azure;
using IMS.Services.ShoppingCartAPI.Models.Dto;
using IMS.Services.ShoppingCartAPI.Data;
using IMS.Services.ShoppingCartAPI.Repository.IRepository;

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using IMS.Services.ShoppingCartAPI.Models.Domain;
using System.Text;
using Microsoft.OpenApi.Validations;

namespace IMS.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly HttpClient httpClient;
        private readonly CartDbContext cartDbContext;
        private readonly IProductRepository productRepository;
        private readonly IAuthRepository authRepository;

        public CartRepository(HttpClient httpClient,CartDbContext cartDbContext,IProductRepository productRepository,IAuthRepository authRepository)
        {
            this.httpClient = httpClient;
            this.cartDbContext = cartDbContext;
            this.productRepository = productRepository;
            this.authRepository = authRepository;
        }


        public async Task<string> UpsertAsync(Guid CartId,Guid productId,string token=null)
        {
            //check if product is valid or not
            var response = await productRepository.GetProductById(productId,token);

            try
            {

                if (response != null && response.IsSuccess)
                {

                    var existingCart = await cartDbContext.Carts.FirstOrDefaultAsync(x => x.Id == CartId);

                    if (existingCart != null)
                    {
                        existingCart.TotalValue += response.Result.Price;
                        existingCart.TotalProductQty += 1;
                    }
                    else
                    {
                        var cart = new Cart { Id = CartId, TotalProductQty = 1, TotalValue = response.Result.Price };
                        await cartDbContext.Carts.AddAsync(cart);
                    }

                    // insert data into cartProducts table

                    //if the product is already inserted into the cart , increase the count
                    var cartProducts = await cartDbContext.CartProducts.FirstOrDefaultAsync(x => x.CartId == CartId && x.ProductId == response.Result.ProductId);
                    if (cartProducts != null)
                    {
                        cartProducts.ProductCount += 1;
                    }
                    //else create new entry in cartProducts table
                    else
                    {
                        var cartProduct = new CartProduct { CartId = CartId, ProductId = response.Result.ProductId, ProductCount = 1 };
                        await cartDbContext.CartProducts.AddAsync(cartProduct);

                    }

                    await cartDbContext.SaveChangesAsync();

                    return $"Added {response.Result.Name} to the Cart";
                    

                }

            }
            catch (Exception ex)
            {
                return "";
            }


            return "";

        }
        public async Task<string> DeleteProductFromCartAsync(Guid cartId, Guid productId,string token=null)
        {
            var product = await productRepository.GetProductById(productId,token);

            try
            {
                if (product.IsSuccess)
                {
                    var cart = await cartDbContext.Carts.FirstOrDefaultAsync(x => x.Id == cartId);
                    var qty = await cartDbContext.CartProducts.FirstOrDefaultAsync(x => x.CartId == cartId && x.ProductId == productId);
                    cart.TotalProductQty -= qty.ProductCount;
                    cart.TotalValue -= (product.Result.Price * qty.ProductCount);

                    var cartProduct = await cartDbContext.CartProducts.FirstOrDefaultAsync(x => x.CartId==cartId && x.ProductId == product.Result.ProductId);
                    cartDbContext.CartProducts.Remove(cartProduct);

                    await cartDbContext.SaveChangesAsync();

                    return $"{product.Result.Name} has been deleted from the Cart";

                }

            }
            catch (Exception ex)
            {
            }

            return "";
        }

        public async Task<ReturnCartDto> GetCartAsync(Guid cartId,string token=null)
        {
            var cart = await cartDbContext.Carts.Include(x=>x.CartProducts).FirstOrDefaultAsync(x=>x.Id==cartId);

            if (cart != null)
            {
                var cartProducts = (from prod in cart.CartProducts
                                   select new
                                   {
                                       productId = prod.ProductId,
                                       productCount = prod.ProductCount
                                   }).ToList();
                // fetch all the products in the cart
                var products = new List<ReturnProductFromCartDto>();
                foreach(var product in cartProducts)
                {
                    var response = await productRepository.GetProductById(product.productId,token);

                    if (response.IsSuccess)
                    {

                        products.Add(new ReturnProductFromCartDto
                        {
                            Name = response.Result.Name,
                            ProductId = response.Result.ProductId,
                            Price = response.Result.Price,
                            CategoryName = response.Result.CategoryName,
                            ProductCount = product.productCount
                        });
                    }
                    
                }
                var result = new ReturnCartDto() { Id = cartId ,TotalValue = cart.TotalValue,TotalProductQty=cart.TotalProductQty,Products=products};
                return result;
                
            }
            return new ReturnCartDto();
        }

        public async Task<string> SendCartByEmailAsync(ReturnCartDto cart,string token=null)
        {

            //generate the email
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

            //get the user's Email id
            var user= await authRepository.GetUserByIdAsync(cart.Id,token);

            if(user.IsSuccess)
            {
                //send the mail
                var res = await authRepository.SendMailAsync(new SendMailRequestDto { Email = user.Result.Email, Subject = subject, Body = emailString },token);
                if(res.IsSuccess)
                    {
                    return "Sent Successfully";
                    }
            }

            return "";
        }
    }
}
