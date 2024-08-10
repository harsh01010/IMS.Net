using IMS.Web.Models;
using IMS.Web.Models.Order;
using IMS.Web.Models.ShoppingCart;
using IMS.Web.Services.IServices;
using IMS.Web.Utility;

namespace IMS.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBaseservice baseservice;

        public OrderService(IBaseservice baseservice)
        {
            this.baseservice = baseservice;
        }
        public async Task<ResponseDto> AddAddressAsync(Guid userId, AddAddressRequestDto addAddressRequestDto)
        {

            return await baseservice.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = addAddressRequestDto,
                Url = StaticDetails.OrderAPIBase + "/api/ShippingAddress/" +userId ,
                ContentType = StaticDetails.ContentType.MultipartFormData
            });

        }

        public async  Task<ResponseDto> DeleteAddressAsync(Guid shippingAddressId)
        {
            return await baseservice.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.OrderAPIBase + "/api/ShippingAddress/" + shippingAddressId
            });
        }

        public async Task<ResponseDto> GetAllAddressAsync(Guid userId)
        {
            return await baseservice.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.OrderAPIBase + "/api/ShippingAddress/" + userId
            });
        }
    }
}
