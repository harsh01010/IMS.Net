using IMS.Services.OrderAPI.Models.DTO;
using IMS.Services.OrderAPI.Service.IService;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace IMS.Services.OrderAPI.Service
{
    public class AuthService:IAuthService
    {

        private readonly HttpClient httpClient;
        protected UserResponseDto responseDto;
        protected ResponseDto responseDto2;

        public AuthService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            responseDto = new UserResponseDto();
            responseDto2 = new ResponseDto();
        }
        public async Task<UserResponseDto> GetUserByIdAsync(Guid id, string token = null)
        {
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await httpClient.GetAsync("https://localhost:7156/api/AuthAPI/getById/" + id);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<UserResponseDto>(jsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            }
            responseDto.IsSuccess = false;
            responseDto.Message = "Error Fetching the Product";
            return responseDto;
        }

        public async Task<ResponseDto> SendMailAsync(SendMailRequestDto sendMailRequestDto, string token = null)
        {
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var jsonContent = JsonSerializer.Serialize(sendMailRequestDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var res = await httpClient.PostAsync("https://localhost:7156/api/EmailService/SendEmail", content);
            if (res.IsSuccessStatusCode)
            {
                var jsonstring = await res.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ResponseDto>(jsonstring, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            }
            else
            {
                responseDto2.IsSuccess = false;
                responseDto2.Message = "Failed";
                return responseDto2;
            }
        }

    }
}
