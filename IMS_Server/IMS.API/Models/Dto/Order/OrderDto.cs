namespace IMS.API.Models.Dto.Order
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public DateTime Ordertime { get; set; }
        public Decimal OrderValue {  get; set; }
        public Boolean Status {  get; set; }

        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();

        public Guid ProductId { get; set; }

    }
}
