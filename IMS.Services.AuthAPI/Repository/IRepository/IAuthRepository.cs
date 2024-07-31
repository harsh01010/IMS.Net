using IMS.Services.AuthAPI.Models.Dto;

namespace IMS.Services.AuthAPI.Repository.IRepository
{
    public interface IAuthRepository
    {
        public Task<String> RegisterAsync(UserRegistrationRequestDto requestDto);
        public Task<LoginResponseDto> LoginAsync(UserLoginRequestDto requestDto);

        public Task<List<UserDto>> GetByRoleAsync(string role);
    }
}