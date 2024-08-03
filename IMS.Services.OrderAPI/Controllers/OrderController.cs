using IMS.Services.OrderAPI.Models;
using IMS.Services.OrderAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Services.OrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("placeorder")]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = // Get user ID from token or session
            var order = await _orderService.PlaceOrderAsync(userId);
            return Ok(order);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetOrderHistory()
        {
            var userId = // Get user ID from token or session
            var orders = await _orderService.GetOrderHistoryAsync(userId);
            return Ok(orders);
        }
    }

}
