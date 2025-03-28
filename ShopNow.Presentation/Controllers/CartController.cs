using Microsoft.AspNetCore.Mvc;

namespace ShopNow.Presentation.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
