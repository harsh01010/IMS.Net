using IMS.Services.AuthAPI.Models.Dto;
using IMS.Services.AuthAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailServiceController : ControllerBase
    {
        private readonly IEmailSender emailSender;
        protected ResponseDto responseDto;

        public EmailServiceController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
            responseDto = new ResponseDto();
        }

        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendEmail(SendEmailRequestDto request)
        {
            var status = await emailSender.EmailSendAsync(request.Email,request.Subject,request.Body);

            if(status)
            {
               responseDto.IsSuccess = true;
                responseDto.Message = "Sent Successfully";
                return Ok(responseDto);
            }
            else
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "Failed to Send";
                return BadRequest(responseDto);
            }
        }
    }
}
