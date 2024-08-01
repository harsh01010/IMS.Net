namespace IMS.Services.AuthAPI.Repository.IRepository
{
    public interface IEmailSender
    {

        Task<bool> EmailSendAsync(string email,string Subject ,string message);
    }
}
