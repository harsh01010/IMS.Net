using IMS.API.Models.Dto;
using IMS.API.Models.Dto.Order;
using IMS.API.Repository.IRepository.IOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderRepository orderRepository;
        protected ResponseDto response;

        public OrdersController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
            response = new ResponseDto();
        }

        [HttpPost]
        [Route("placeOrder/{cartId:Guid}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> PlaceOrder([FromRoute] Guid cartId, [FromBody] PlaceOrderRequestDto placeOrderRequestDto)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var responseString = await orderRepository.PlaceOrderAsync(cartId, placeOrderRequestDto.shippingAddressId, token);
            if (String.IsNullOrEmpty(responseString))
            {
                response.IsSuccess = false;
                response.Message = "failed to place order";
                return BadRequest(response);
            }
            else
            {
                response.IsSuccess = true;
                response.Message = responseString;
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("getAllOrders")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await orderRepository.GetAllOrdersAsync();

            response.IsSuccess = true;
            response.Result = orders;
            response.Message = "fetched successfully";
            return Ok(response);
        }



    }
}
