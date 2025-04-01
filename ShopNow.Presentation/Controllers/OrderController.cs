using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.OrderViewModel;

namespace ShopNow.Presentation.Controllers
{
	public class OrderController(IOrderService orderService) : Controller
	{
		[HttpGet("Order/{orderId:guid}")]
		public async Task<IActionResult> OrderDetail(Guid orderId)
		{
			OrderDetailViewModel model = new OrderDetailViewModel()
			{
				OrderDetailDTO = await orderService.GetOrderDetailReport(orderId)
			};
			return View(model);
		}
	}
}
