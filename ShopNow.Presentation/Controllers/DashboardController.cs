using Microsoft.AspNetCore.Mvc;

namespace ShopNow.Presentation.Controllers
{
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
