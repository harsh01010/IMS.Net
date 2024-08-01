namespace IMS.Services.ShoppingCartAPI.Models.Domain
{
    public class Cart
    {
        public Guid Id { get; set; } // same as customer id

        public int TotalProductQty {  get; set; }

        public double TotalValue {  get; set; }

        public  ICollection<CartProduct> CartProducts { get; set; }//one to many with cartProduct

    }
}
