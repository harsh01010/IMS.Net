namespace IMS.API.Models.Dto.Order
{
    public class OrderDetailsDto
    {
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime OrderTime { get; set; }

        public bool Status { get; set; }

        public double OrderValue { get; set; }
    }
}
