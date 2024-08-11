using IMS.Services.ShoppingCartAPI.Models.Dto;

namespace IMS.Services.ShoppingCartAPI.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<UserResponseDto> GetUserByIdAsync(Guid id,string token=null);
        Task<ResponseDto> SendMailAsync(SendMailRequestDto sendMailRequestDto,string token=null);
    }
}
