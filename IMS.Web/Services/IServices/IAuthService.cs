using IMS.Web.Models;

namespace IMS.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(UserLoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(UserRegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(UserRegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> DeleteAsync(userDeleteRequestDto userDeleteRequestDto);


    }
}
