using IMS.API.Models.Dto;

namespace IMS.API.Repository.IRepository.IAuth
{
    public interface IEmailSender
    {

        Task<bool> EmailSendAsync(SendEmailRequestDto requestDto);
    }
}
