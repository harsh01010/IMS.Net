namespace IMS.Services.AuthAPI.Models.Dto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }

        public string JwtToken {  get; set; }
    }
}
