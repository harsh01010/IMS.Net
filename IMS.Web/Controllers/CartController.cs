using IMS.Web.Models;
using IMS.Web.Models.ShoppingCart;
using IMS.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace IMS.Web.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartService cartService;
		private readonly ITokenProvider tokenProvider;

		public CartController(ICartService cartService,ITokenProvider tokenProvider)

        {
			this.cartService = cartService;
			this.tokenProvider = tokenProvider;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCart(CartRequestDto cartRequestDto)
		{
			if (ModelState.IsValid)
			{
				var id = Guid.Parse(tokenProvider.GetId());
				ResponseDto? response = await cartService.UpsertAsync(id, cartRequestDto.ProductId);

				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Added to cart Successfully";
				}
				else
				{
					TempData["error"] = response?.Message;
				}
			}

			return RedirectToAction("ProductIndex", "Product"); ;
		}

		[HttpGet]
		public async Task<IActionResult> GetCart()
		{
			
				return View(await LoadCartDtoBasedOnLoggedInUser());
			
		}
		public IActionResult Index()
		{
			return View();
		}

        private async Task<ReturnCartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await cartService.GetCartAsync(Guid.Parse(userId));
            if (response != null & response.IsSuccess)
            {
                ReturnCartDto cartDto = JsonConvert.DeserializeObject<ReturnCartDto>(Convert.ToString(response.Result));
                return cartDto;
            }
            return new ReturnCartDto();
        }

		[HttpPost]
        public async Task<IActionResult> Remove(Guid productId)
        {
            if (ModelState.IsValid)
            {
                var id = Guid.Parse(tokenProvider.GetId());
                ResponseDto? response = await cartService.DeleteProductFromCartAsync(id,productId);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Deleted Succesfully";
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

            return RedirectToAction("GetCart", "Cart"); ;
        }
		
    }
}
