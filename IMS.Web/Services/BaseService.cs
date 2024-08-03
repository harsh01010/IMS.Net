using IMS.Web.Models;
using IMS.Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static IMS.Web.Utility.StaticDetails;

namespace IMS.Web.Services
{
    public class BaseService : IBaseservice
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                var client = httpClientFactory.CreateClient("Harsh_API");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //token making

                message.RequestUri = new Uri(requestDto.Url);

                if (requestDto.Data != null)
                {

                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");

                }

                HttpResponseMessage? resp = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                resp = await client.SendAsync(message);

                switch (resp.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await resp.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var Dto = new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message.ToString(),

                };
                return Dto;
               
            }

        }
    }
}
