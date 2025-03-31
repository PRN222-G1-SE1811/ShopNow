using Microsoft.AspNetCore.Mvc;

namespace ShopNow.Presentation.Controllers
{
	public class OrderController : Controller
	{
		[HttpGet("Order/{orderId:guid}")]
		public IActionResult OrderDetail(Guid orderId)
		{
			return View();
		}
	}
}
