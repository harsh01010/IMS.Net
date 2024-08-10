using IMS.Web.Models;
using IMS.Web.Models.ShoppingCart;
using IMS.Web.Services.IServices;
using IMS.Web.Utility;

namespace IMS.Web.Services
{
	public class CartService : ICartService
	{
		private readonly IBaseservice _baseService;
		public CartService(IBaseservice baseService)
		{
			_baseService = baseService;
		}
		public async Task<ResponseDto> DeleteProductFromCartAsync(Guid cartId, Guid productId)
		{
            var req = new CartRequestDto { ProductId = productId };
            return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = StaticDetails.ApiType.DELETE,
                Data = req,
                Url = StaticDetails.ShoppingCartAPIBase + "/api/Cart/delete/" + cartId
			});
        }

		public  async Task<ResponseDto> GetCartAsync(Guid cartId, string token = null)
		{
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.ShoppingCartAPIBase + "/api/Cart/get/" + cartId
            });
        }

		public async Task<ResponseDto> UpsertAsync(Guid cartId, Guid productId)
		{
			var req = new CartRequestDto { ProductId = productId };
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = StaticDetails.ApiType.POST,
				Data = req ,
				Url = StaticDetails.ShoppingCartAPIBase + "/api/Cart/upsert/"+cartId,
				ContentType = StaticDetails.ContentType.MultipartFormData
			});
		}
	}
}
