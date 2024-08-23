namespace IMS.API.Models.Domain.Order
{
    public class OrderItemModel
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int ProductCount { get; set; }

        //navigation property
        public OrderModel order { get; set; }
    }
}
