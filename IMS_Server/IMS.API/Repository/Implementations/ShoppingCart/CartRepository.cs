using IMS.API.Models.Dto;
using IMS.API.Models.Dto.ShoppingCart;
using IMS.API.Repository.IRepository.IAuth;
using IMS.API.Repository.IRepository.IProduct;
using IMS.API.Repository.IRepository.IShoppingCart;
using IMS.API.Data;
using IMS.API.Models.Domain.ShoppingCart;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace IMS.API.Repository.Implementations.ShoppingCart
{
    public class CartRepository:ICartRepository
    {

        
        private readonly IMSDbContext db;
        private readonly IEmailSender emailSender;
        private readonly IProductRepository productRepository;
        private readonly IAuthRepository authRepository;

        public CartRepository( IMSDbContext db,IEmailSender emailSender,IAuthRepository authRepository)
        {
            this.db = db;
            this.emailSender = emailSender;
            this.authRepository = authRepository;
        }


        public async Task<string> UpsertAsync(Guid CartId, Guid productId, string token = null)
        {
            //check if product is valid or not
            var response = await db.Products.FirstOrDefaultAsync(x=>x.ProductId==productId);

            try
            {

                if (response != null)
                {

                    var existingCart = await db.Carts.FirstOrDefaultAsync(x => x.Id == CartId);

                    if (existingCart != null)
                    {
                        existingCart.TotalValue += response.Price;
                        existingCart.TotalProductQty += 1;
                    }
                    else
                    {
                        var cart = new CartModel { Id = CartId, TotalProductQty = 1, TotalValue = response.Price };
                        await db.Carts.AddAsync(cart);
                    }

                    // insert data into cartProducts table

                    //if the product is already inserted into the cart , increase the count
                    var cartProducts = await db.CartProducts.FirstOrDefaultAsync(x => x.CartId == CartId && x.ProductId == response.ProductId);
                    if (cartProducts != null)
                    {
                        cartProducts.ProductCount += 1;
                    }
                    //else create new entry in cartProducts table
                    else
                    {
                        var cartProduct = new CartProductModel { CartId = CartId, ProductId = response.ProductId, ProductCount = 1 };
                        await db.CartProducts.AddAsync(cartProduct);

                    }

                    await db.SaveChangesAsync();

                    return $"Added {response.Name} to the Cart";


                }

            }
            catch (Exception ex)
            {
                return "";
            }


            return "";

        }
        public async Task<string> DeleteProductFromCartAsync(Guid cartId, Guid productId, string token = null)
        {
            var product = await db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            try
            {
                if (product != null)
                {
                    var cart = await db.Carts.FirstOrDefaultAsync(x => x.Id == cartId);
                    var qty = await db.CartProducts.FirstOrDefaultAsync(x => x.CartId == cartId && x.ProductId == productId);
                    cart.TotalProductQty -= qty.ProductCount;
                    cart.TotalValue -= (product.Price * qty.ProductCount);

                    var cartProduct = await db.CartProducts.FirstOrDefaultAsync(x => x.CartId == cartId && x.ProductId == product.ProductId);
                    db.CartProducts.Remove(cartProduct);

                    await db.SaveChangesAsync();

                    return $"{product.Name} has been deleted from the Cart";

                }

            }
            catch (Exception ex)
            {
            }

            return "";
        }

        public async Task<ReturnCartDto> GetCartAsync(Guid cartId, string token = null)
        {
            var cart = await db.Carts.Include(x => x.CartProducts).FirstOrDefaultAsync(x => x.Id == cartId);

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
                foreach (var product in cartProducts)
                {
                    var response = await db.Products.FirstOrDefaultAsync(x => x.ProductId == product.productId);

                    if (response != null)
                    {

                        products.Add(new ReturnProductFromCartDto
                        {
                            Name = response.Name,
                            ProductId = response.ProductId,
                            Price = response.Price,
                            CategoryName = response.CategoryName,
                            ProductCount = product.productCount
                        });
                    }

                }
                var result = new ReturnCartDto() { Id = cartId, TotalValue = cart.TotalValue, TotalProductQty = cart.TotalProductQty, Products = products };
                return result;

            }
            return new ReturnCartDto();
        }


        public async Task<string> SendCartByEmailAsync(ReturnCartDto cart, string token = null)
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
            var user = await authRepository.GetById(cart.Id);

            if (user != null)
            {
                //send the mail
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
            try
            {
                var cart = await db.Carts.FirstOrDefaultAsync(curr=>curr.Id == cartId);
                if (cart != null)
                {
                    db.Carts.Remove(cart);
                    await db.SaveChangesAsync();
                    return true;
                }
                else return false;

            }
            catch
            {
                return false;
            }
        }
    }
}
