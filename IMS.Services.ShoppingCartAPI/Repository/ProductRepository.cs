using IMS.Services.ShoppingCartAPI.Models.Dto;
using IMS.Services.ShoppingCartAPI.Models.Dto;
using IMS.Services.ShoppingCartAPI.Repository.IRepository;
using System.Text.Json;

namespace IMS.Services.ShoppingCartAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly HttpClient httpClient;
        protected ProductResponseDto responseDto;
        public ProductRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            responseDto = new ProductResponseDto();
        }
        public async Task<ProductResponseDto> GetProductById(Guid id)
        {

            var response = await httpClient.GetAsync($"https://localhost:7193/api/product/{id}");

            if(response.IsSuccessStatusCode)
            {
                var jsonString= await response.Content.ReadAsStringAsync();


                return JsonSerializer.Deserialize<ProductResponseDto>(jsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
               
            }
            responseDto.IsSuccess = false;
            responseDto.Message = "Error Fetching the Product";
            return responseDto;
            
        }
    }
}
