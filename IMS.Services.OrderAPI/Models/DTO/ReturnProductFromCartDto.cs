namespace IMS.Services.ShoppingCartAPI.Models.Dto
{
    public class ReturnProductFromCartDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public int ProductCount { get; set; }
    }
}
