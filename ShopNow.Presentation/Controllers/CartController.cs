using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.CartViewModel;
using ShopNow.Shared.Enums;
using System.Security.Claims;

namespace ShopNow.Presentation.Controllers
{
	//[Authorize(Roles = RoleName.Member)]
	public class CartController(ICartService cartService) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			var cart = await cartService.GetCart(Guid.Parse(userId!));
			CartViewModel model = new CartViewModel() { CartDTO = cart};
			return View(model);
		}

		//[Authorize(Roles = RoleName.Member)]
		[HttpPost]
		public async Task<IActionResult> AddToCart([FromBody] AddToCartViewModel model)
		{
			string message = "Add to cart fail!";
			if (ModelState.IsValid)
			{
				var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
				await cartService.AddToCart(Guid.Parse(userId!), model.Item);
				message = "Add to cart successfully!";
			}

			return Json(new { Message = message });
		}
	}
}
