using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.CartViewModel;
using ShopNow.Shared.Enums;
using System.Security.Claims;

namespace ShopNow.Presentation.Controllers
{
	public class CartController(ICartService cartService) : Controller
	{
		public IActionResult Index()
		{
			return View();
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
