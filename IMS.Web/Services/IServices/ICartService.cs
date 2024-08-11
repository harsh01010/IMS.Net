using IMS.Web.Models;

namespace IMS.Web.Services.IServices
{
	public interface ICartService
	{
		Task<ResponseDto> UpsertAsync(Guid cartId, Guid productId);
		Task<ResponseDto> DeleteProductFromCartAsync(Guid cartId, Guid productId);

		Task<ResponseDto> GetCartAsync(Guid cartId, string token = null);

		Task<ResponseDto> MailCartAsync(Guid cartId);

	}
}
