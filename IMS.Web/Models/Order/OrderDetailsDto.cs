namespace IMS.Web.Models.Order
{
    public class OrderDetailsDto
    {
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime OrderTime { get; set; }

        public Boolean Status { get; set; }

        public Double OrderValue { get; set; }
    }

}
