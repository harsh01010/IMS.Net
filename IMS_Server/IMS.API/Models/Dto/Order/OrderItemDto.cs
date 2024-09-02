namespace IMS.API.Models.Dto.Order
{
    public class OrderItemDto
    {
        public Guid ProductId {  get; set; }
        public string Name {  get; set; }
        public Decimal Price { get; set; }
        public string ImageUrl {  get; set; }
        public Guid CategoryId { get; set; }
        public string ProductCount {  get; set; }
    }
}
