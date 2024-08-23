namespace IMS.API.Models.Domain.ShoppingCart
{
    public class CartModel
    {
        public Guid Id { get; set; } // same as customer id

        public int TotalProductQty { get; set; }

        public double TotalValue { get; set; }

        public ICollection<CartProductModel> CartProducts { get; set; }//one to many with cartProduct
    }
}
