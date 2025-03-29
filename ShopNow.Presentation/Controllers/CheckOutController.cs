using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.CheckOutViewModel;
using ShowNow.Domain.Entities;
using System.Security.Claims;

namespace ShopNow.Presentation.Controllers
{
	public class CheckOutController(IUserService userService, IShippingService shippingService) : Controller
	{
		public IActionResult Index([FromBody] CheckOutViewModel model)
		{
			if (!ModelState.IsValid)
			{
				
			}
			HttpContext.Session.SetString("CheckOutData", JsonConvert.SerializeObject(model));
			return Json(new { redirectUrl = Url.Action("Confirmation", "CheckOut") });
		}

		public async Task<IActionResult> Confirmation()
		{
			var checkoutJson = HttpContext.Session.GetString("CheckOutData");
			if (string.IsNullOrEmpty(checkoutJson))
			{
				return RedirectToAction("Index"); // Redirect if no data
			}

			var order = JsonConvert.DeserializeObject<CheckOutViewModel>(checkoutJson);
			var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			var user = await userService.GetUserDetail(Guid.Parse(userId!));
			ConfirmationViewModel model = new ConfirmationViewModel()
			{
				UserDetailDTO = user,
				Items = order!.Items
			};
			var totalShip = await shippingService.CalculateShippingFee();
			return View(model);
		}
	}
}
