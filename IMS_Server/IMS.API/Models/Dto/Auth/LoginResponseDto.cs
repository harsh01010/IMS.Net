namespace IMS.API.Models.Dto.Auth
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }

        public string JwtToken { get; set; }
    }
}
