using IMS.Web.Models;
using IMS.Web.Models.Order;
using IMS.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace IMS.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ITokenProvider tokenProvider;

        public OrderController(IOrderService orderService, ITokenProvider tokenProvider)

        {
            this.orderService = orderService;
            this.tokenProvider = tokenProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddress()
        {

            return View(await LoadCartDtoBasedOnLoggedInUser());

        }

        [HttpPost]

        public async Task<IActionResult> AddAddress(AddAddressRequestDto addAddressRequestDto)
        {
            if (ModelState.IsValid)
            {
                var id = Guid.Parse(tokenProvider.GetId());
                ResponseDto? response = await orderService.AddAddressAsync(id,addAddressRequestDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Address Added Successfully";
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

            return RedirectToAction("GetAddress", "Order"); ;
        }



        public IActionResult Details()
        {
            return View();
        }

        private async Task<List<ReturnShippingAddressDto>> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await orderService.GetAllAddressAsync(Guid.Parse(userId));
            if (response != null & response.IsSuccess)
            {
                var shippingAddressDto = JsonConvert.DeserializeObject<List<ReturnShippingAddressDto>>(Convert.ToString(response.Result));
                return shippingAddressDto;
            }
            return new List<ReturnShippingAddressDto>();
        }
        public async Task<IActionResult> Remove(Guid shippingAddressId)
        {
            if (ModelState.IsValid)
            {
                //var id = Guid.Parse(tokenProvider.GetId());
                ResponseDto? response = await orderService.DeleteAddressAsync(shippingAddressId);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Deleted Succesfully";
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

            return RedirectToAction("GetAddress", "Order");
        }
    }
}
