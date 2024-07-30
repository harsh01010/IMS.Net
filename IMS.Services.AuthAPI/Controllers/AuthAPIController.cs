using IMS.Services.AuthAPI.Models.Dto;
using IMS.Services.AuthAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Services.AuthAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        protected ResponseDto responseDto;

        public AuthAPIController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
            responseDto = new ResponseDto();
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            var message = await authRepository.RegisterAsync(requestDto);

            if (!string.IsNullOrEmpty(message))
            {
                responseDto.IsSuccess = false;
                responseDto.Message = message;
                return BadRequest(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login()
        {
            return Ok();
        }


    }
}
