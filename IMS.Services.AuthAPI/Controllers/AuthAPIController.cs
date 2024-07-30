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
            var registerMessage = await authRepository.RegisterAsync(requestDto);

            if (!string.IsNullOrEmpty(registerMessage))
            {
                responseDto.IsSuccess = false;
                responseDto.Message = registerMessage;
                return BadRequest(responseDto);
            }
            else
            {
                responseDto.IsSuccess = true;
                responseDto.Message = "Registered Successfully";
            }
            return Ok(responseDto);
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login([FromBody]UserLoginRequestDto requestDto)
        {
            
            var loginResponse = await authRepository.LoginAsync(requestDto);
            if (loginResponse.User == null)
            {

                responseDto.IsSuccess = false;
                responseDto.Message = "Username or Password is Incorrect";

                return BadRequest(responseDto);

            }
            else
            {
                responseDto.IsSuccess = true;
                responseDto.Message = "Login Success";
                responseDto.Result = loginResponse;
                return Ok(responseDto);
            }

            
        }


    }
}
