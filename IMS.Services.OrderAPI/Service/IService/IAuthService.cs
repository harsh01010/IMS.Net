using IMS.Services.OrderAPI.Models.DTO;



namespace IMS.Services.OrderAPI.Service.IService
{
    public interface IAuthService
    {
        Task<UserResponseDto> GetUserByIdAsync(Guid id, string token = null);
        Task<ResponseDto> SendMailAsync(SendMailRequestDto sendMailRequestDto, string token = null);
    }
}
