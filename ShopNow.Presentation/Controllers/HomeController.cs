using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;
using System.Diagnostics;

namespace ShopNow.Presentation.Controllers
{
	// test NghiNVkkkk 
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork<Product, Guid> _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork<Product, Guid> unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			return View();
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
