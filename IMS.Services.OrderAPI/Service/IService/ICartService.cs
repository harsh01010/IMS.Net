using IMS.Services.ShoppingCartAPI.Models.Dto;

namespace IMS.Services.OrderAPI.Service.IService
{
    public interface ICartService
    {
        public Task<ReturnCartDto> GetCartById(Guid id);
    }
}
