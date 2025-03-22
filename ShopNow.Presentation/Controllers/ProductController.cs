using Microsoft.AspNetCore.Mvc;

namespace ShopNow.Presentation.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Manage()
		{
			ViewData["active"] = "product";
			return View();
		}
	}
}
