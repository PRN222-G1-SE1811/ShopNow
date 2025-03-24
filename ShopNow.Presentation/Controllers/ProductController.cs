using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.ProductViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Shared.Enums;

namespace ShopNow.Presentation.Controllers
{
	public class ProductController(ICategoryService categoryService) : Controller
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

		public async Task<IActionResult> Create()
		{
			CreateProductViewModel model = new CreateProductViewModel();
			model.Categories = new SelectList(await categoryService.GetSelectListCategories(), "Id", "Name");
			model.Featured = new SelectList(new ProductFeatured[] { ProductFeatured.No, ProductFeatured.Yes });
			return View(model);
		}

		[HttpPost]

		public IActionResult Create(CreateProductViewModel model)
		{

			return RedirectToAction("Manage");
		}
	}
}
