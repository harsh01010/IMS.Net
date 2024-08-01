using System.Text.Json.Serialization;

namespace IMS.Services.ShoppingCartAPI.Models.Dto
{
    public class ResponseDto
    {
        [JsonPropertyName("result")]
        public object? Result {  get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; } = true;

        [JsonPropertyName("message")]
        public string Message { get; set; } = "";
    }
}
