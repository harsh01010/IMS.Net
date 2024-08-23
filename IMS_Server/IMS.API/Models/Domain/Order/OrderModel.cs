using System.ComponentModel.DataAnnotations;

namespace IMS.API.Models.Domain.Order
{
    public class OrderModel
    {
        [Key]
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime OrderTime { get; set; }

        public Boolean Status { get; set; }

        public Double OrderValue { get; set; }


        public ICollection<OrderItemModel> Items { get; set; }
    }
}
