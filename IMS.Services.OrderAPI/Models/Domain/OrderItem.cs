namespace IMS.Services.OrderAPI.Models.Domain
{
    public class OrderItem
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int ProductCount { get; set; }

        public Order order { get; set; }
    }
}
