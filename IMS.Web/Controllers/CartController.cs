using IMS.Web.Models;
using IMS.Web.Models.ShoppingCart;
using IMS.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

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
		public IActionResult Index()
		{
			return View();
		}
	}
}
