namespace IMS.API.Models.Domain.ShippingAddress
{
    public class ShippingAddressModel
    {
        public Guid shippingAddressId { get; set; }

        public Guid userId { get; set; }

        public string houseNo { get; set; }

        public string street { get; set; }

        public string pinCode { get; set; }

        public string city { get; set; }

        public string state { get; set; }

    }
}
