namespace IMS.Services.AuthAPI.Models.Dto
{
    public class SendEmailRequestDto
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
