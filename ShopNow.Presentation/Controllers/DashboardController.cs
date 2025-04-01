using Microsoft.AspNetCore.Mvc;
using ShopNow.Infrastructure.Data;

namespace ShopNow.Presentation.Controllers
{
	public class DashboardController : Controller
	{
		private readonly ShopNowDbContext _context;

        public DashboardController(ShopNowDbContext context)
        {
			_context = context;
        }

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public JsonResult GetOrdersByStatus()
		{
			var statusCounts = _context.Orders
				.GroupBy(o => o.OrderStatus)
				.Select(g => new { Status = g.Key, Count = g.Count() })
				.ToList();

			return Json(statusCounts);
		}

        [HttpGet]
        public JsonResult GetOrdersByMonth()
        {
            var startDate = DateTime.Now.AddMonths(-6);

            var monthlyOrders = _context.Orders
                .Where(o => o.CreatedAt >= startDate)
                .AsEnumerable() // ✅ Forces in-memory processing (EF can't translate Year/Month in GroupBy)
                .GroupBy(o => new { o.CreatedAt.Year, o.CreatedAt.Month })
                .Select(g => new {
                    Month = $"{g.Key.Year}-{g.Key.Month}",
                    Count = g.Count(),
                    Revenue = g.Sum(o => o.TotalCost)
                })
                .OrderBy(x => x.Month)
                .ToList();

            return Json(monthlyOrders);
        }


        [HttpGet]
		public JsonResult GetOrderFulfillmentTimes()
		{
			var fulfillmentData = _context.Orders
				.Where(o => o.CompletedAt.HasValue)
				.Select(o => new {
					OrderId = o.Id,
					DaysToComplete = (o.CompletedAt.Value - o.CreatedAt).TotalDays
				})
				.ToList();

			return Json(fulfillmentData);
		}

		[HttpGet]
		public JsonResult GetPaymentMethodDistribution()
		{
			var paymentMethods = _context.Orders
				.GroupBy(o => o.PaymentMethod)
				.Select(g => new { Method = g.Key, Count = g.Count() })
				.ToList();

			return Json(paymentMethods);
		}

		
	}
}
