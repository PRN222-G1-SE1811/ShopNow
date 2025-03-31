using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.CheckOutViewModel;
using ShowNow.Domain.Entities;
using System.Security.Claims;

namespace ShopNow.Presentation.Controllers
{
	public class CheckOutController(IUserService userService, IOrderService orderService) : Controller
	{
		[HttpPost]
		public IActionResult Index([FromBody] CheckOutViewModel model)
		{
			if (!ModelState.IsValid)
			{

			}
			HttpContext.Session.SetString("CheckOutData", JsonConvert.SerializeObject(model));
			return Json(new { redirectUrl = Url.Action("Confirmation", "CheckOut") });
		}

		[HttpGet]
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
			//var provinces = await shippingService.GetProvinces();
			//var districts = await shippingService.GetDistrictsByProvince(201);
			//var wards = await shippingService.GetWardsByDistrict(1566);
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Confirmation(ConfirmationViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(Confirmation));
			}
			
			var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			model.UserDetailDTO.Id = Guid.Parse(userId!);
			var id = await orderService.CreateOrder(model.Items, model.UserDetailDTO, model.PaymentMethod, model.ShippingFee);
			if (model.PaymentMethod == Shared.Enums.PaymentMethod.VNPay)
			{
				return RedirectToAction("CreatePaymentUrl", "Payment", id);
			}
			return View();
		}

		[HttpGet]
		public IActionResult PaymentFailed()
		{
			return View("/Views/Error/PaymentFailed.cshtml");
		}

		[HttpGet]
		public IActionResult PaymentSuccesed()
		{
			return View("/Views/Error/PaymentSuccessed.cshtml");
		}

	}
}
