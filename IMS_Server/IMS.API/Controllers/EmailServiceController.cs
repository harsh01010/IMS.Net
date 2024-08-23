using IMS.API.Models.Dto;
using IMS.API.Models.Dto.Auth;
using IMS.API.Repository.IRepository.IAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Controllers
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
            var status = await emailSender.EmailSendAsync(request);

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
