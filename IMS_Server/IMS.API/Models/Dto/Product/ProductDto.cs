namespace IMS.API.Models.Dto.Product
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }

        public int AvailableQuantity;
        public string Description { get; set; } = string.Empty;

        public string? CategoryID {  get; set; }
        public string? CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        public IFormFile? Image { get; set; }
    }
}
