using System.Text.Json.Serialization;

namespace IMS.Services.OrderAPI.Models.DTO
{
    public class UserResponseDto
    {
        [JsonPropertyName("result")]
        public UserDto Result { get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; } = true;

        [JsonPropertyName("message")]
        public string Message { get; set; } = "";
    }
}
