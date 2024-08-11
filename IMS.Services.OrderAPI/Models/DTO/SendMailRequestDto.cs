namespace IMS.Services.OrderAPI.Models.DTO
{
    public class SendMailRequestDto
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
