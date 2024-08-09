using IMS.Services.OrderAPI.Models.DTO;
using IMS.Services.OrderAPI.Service.IService;
using IMS.Services.ShoppingCartAPI.Models.Dto;
using System.Net.Http.Headers;
using System.Text.Json;

namespace IMS.Services.OrderAPI.Service
{
    public class CartService : ICartService
    {
        private readonly HttpClient httpClient;

        public CartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ReturnCartDto> GetCartById(Guid id,string token=null)
        {
            if (!String.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await httpClient.GetAsync($"https://localhost:7209/api/Cart/get/{id}");
            if(response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var cartResponse = JsonSerializer.Deserialize<ResponseDto>(jsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                if (cartResponse != null && cartResponse.IsSuccess)
                {
                    var resultEle =JsonSerializer.Serialize(cartResponse.Result);
                    var returnCartDto = JsonSerializer.Deserialize<ReturnCartDto>(resultEle, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                    return returnCartDto;
                }

            }
            return new ReturnCartDto();
        }
    }
}
