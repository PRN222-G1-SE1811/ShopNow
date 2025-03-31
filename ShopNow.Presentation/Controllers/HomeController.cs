using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models;
using System.Diagnostics;

namespace ShopNow.Presentation.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IProductService _productService;

		public HomeController(ILogger<HomeController> logger, IProductService productService)
		{
			_logger = logger;
			_productService = productService;
		}

		public async Task<IActionResult> IndexAsync()
		{
            if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("Manage", "Product");
            }
            else
            {
				var products = await _productService.GetAllProductsAsync();
                return View(products);
            }
        }

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
