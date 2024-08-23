using IMS.API.Models.Dto.Auth;

namespace IMS.API.Repository.IRepository.IAuth
{
    public interface IAuthRepository
    {
        public Task<string> RegisterAsync(UserRegistrationRequestDto requestDto);
        public Task<LoginResponseDto> LoginAsync(UserLoginRequestDto requestDto);

        public Task<List<UserDto>> GetByRoleAsync(string role);

        public Task<UserDto> GetById(Guid id);
    }
}