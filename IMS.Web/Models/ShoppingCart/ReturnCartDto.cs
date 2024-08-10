namespace IMS.Web.Models.ShoppingCart
{
    public class ReturnCartDto
    {
        public Guid Id { get; set; } // same as customer id

        public int TotalProductQty { get; set; }

        public double TotalValue { get; set; }

        public List<ReturnProductFromCartDto> Products { get; set; }
    }
}
