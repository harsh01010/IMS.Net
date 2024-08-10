using System.Text.Json.Serialization;

namespace IMS.Web.Models.ShoppingCart
{
    public class ProductResponseDto
    {
        [JsonPropertyName("result")]
        public CartProductDto Result { get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; } = true;

        [JsonPropertyName("message")]
        public string Message { get; set; } = "";
    }
}
