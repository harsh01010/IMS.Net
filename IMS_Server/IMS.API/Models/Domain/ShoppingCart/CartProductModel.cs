using IMS.API.Models.Domain.Product;

namespace IMS.API.Models.Domain.ShoppingCart
{
    public class CartProductModel
    {
        public Guid CartId { get; set; }

        public Guid ProductId { get; set; }

        public int ProductCount { get; set; }


        // Navigation Property
        public CartModel Cart { get; set; } 
        public  ProductModel Product { get; set; }
    }
}
