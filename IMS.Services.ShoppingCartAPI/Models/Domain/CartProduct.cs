namespace IMS.Services.ShoppingCartAPI.Models.Domain
{
    public class CartProduct
    {
        public Guid Id { get; }
        public Guid CartId { get; set; }

        public Guid ProductId { get; set; }

        public int ProductCount { get; set; }

        public Cart Cart { get; set; } // Navigation Property
    }
}
