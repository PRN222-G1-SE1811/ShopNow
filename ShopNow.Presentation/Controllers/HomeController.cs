using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopNow.Application.Services.Implements;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Infrastructure.Data;
using ShopNow.Presentation.Models;
using ShowNow.Domain.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ShopNow.Presentation.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ShopNowDbContext _shopNowDbContext;

	

		public HomeController(ILogger<HomeController> logger, ShopNowDbContext context)
		{
			_logger = logger;
			_shopNowDbContext = context;
		}

		public async Task<IActionResult> Index()
		{
            if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("Manage", "Product");
            }
            else
            {
				var products = await _shopNowDbContext.Products.ToListAsync();
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
