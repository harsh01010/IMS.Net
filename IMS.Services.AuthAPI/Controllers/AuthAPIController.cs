using IMS.Services.AuthAPI.Models.Dto;
using IMS.Services.AuthAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Services.AuthAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly IEmailSender emailSender;
        protected ResponseDto responseDto;

        public AuthAPIController(IAuthRepository authRepository, IEmailSender emailSender)
        {
            this.authRepository = authRepository;
            this.emailSender = emailSender;
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
                bool status = await  emailSender.EmailSendAsync(requestDto.Email, "Account Created at IMS Portal", $"Congratulations {requestDto.Name} your account is successfully Registered");
               
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

        [HttpGet]
        [Route("getByRole/{role}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllByRole([FromRoute]string role)
        {
            List<UserDto> usersByRole = await authRepository.GetByRoleAsync(role);

            if(!usersByRole.IsNullOrEmpty())
            {
                responseDto.IsSuccess = true;
                responseDto.Message = "Success";
                responseDto.Result = usersByRole;

                return Ok(responseDto);
            }
            else
            {
                responseDto.IsSuccess =false;
                responseDto.Message = $"No Users With {role} Role";

                return NotFound(responseDto);
            }
        }



        [HttpGet]
        [Route("getById/{id:guid}")]
        [Authorize(Roles ="Admin,Customer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await authRepository.GetById(id);
            if (user != null)
            {
                responseDto.IsSuccess = true;
                responseDto.Result= user;
                responseDto.Message = "Fetched successfully";
                return Ok(responseDto); 
            }
            else
            {
                responseDto.IsSuccess=false;
                responseDto.Message = "Something Went Wrong";
                return BadRequest(responseDto);
            }
           
        }

    }
}
