namespace IMS.Web.Models.Order
{
    public class ReturnShippingAddressDto
    {
        public Guid shippingAddressId { get; set; }

        public string houseNo { get; set; }

        public string street { get; set; }

        public string pinCode { get; set; }

        public string city { get; set; }

        public string state { get; set; }
    }
}
