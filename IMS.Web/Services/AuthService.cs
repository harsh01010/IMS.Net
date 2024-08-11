using IMS.Web.Models;
using IMS.Web.Services.IServices;
using IMS.Web.Utility;

namespace IMS.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseservice baseservice;

        public AuthService(IBaseservice baseservice)
        {
            this.baseservice = baseservice;
        }

        public async Task<ResponseDto?> AssignRoleAsync(UserRegistrationRequestDto registrationRequestDto)
        {
            return await baseservice.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticDetails.AuthAPIBase + $"/api/AuthAPI/{registrationRequestDto.Role}"
            });
        }

        //public async Task<ResponseDto?> DeleteAsync(UserDeleteRequestDto userDeleteRequestDto)
        //{
        //    return await baseservice.SendAsync(new RequestDto()
        //    {
        //        ApiType = StaticDetails.ApiType.DELETE,
        //        Data = userDeleteRequestDto,
        //        Url = StaticDetails.AuthAPIBase + "/api/AuthAPI/DeleteUser"
        //    });
        //}

        public async Task<ResponseDto?> LoginAsync(UserLoginRequestDto loginRequestDto)
        {
            return await baseservice.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = loginRequestDto,
                Url = StaticDetails.AuthAPIBase + "/api/AuthAPI/Login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(UserRegistrationRequestDto registrationRequestDto)
        {
            return await baseservice.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticDetails.AuthAPIBase + "/api/AuthAPI/Register"
            }, withBearer: false);
        }
        
    }
}
