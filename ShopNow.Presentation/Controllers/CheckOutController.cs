﻿using Microsoft.AspNetCore.Identity;
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
			return View();
		}
	}
}
