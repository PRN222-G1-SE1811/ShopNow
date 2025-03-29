using Microsoft.AspNetCore.Mvc;
using ShopNow.Presentation.Models.CheckOutViewModel;
using System.Security.Claims;

namespace ShopNow.Presentation.Controllers
{
	public class CheckOutController : Controller
	{
		//public IActionResult Index(CheckOutViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
		//	}
		//	return View();
		//}

		public IActionResult Index()
		{
			return View();
		}
	}
}
