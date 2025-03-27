using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.ProductViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Shared.Enums;
using ShopNow.Application.DTOs.Categories;

namespace ShopNow.Presentation.Controllers
{
	public class ProductController(ICategoryService categoryService, IProductService productService) : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		#region manage
		public async Task<IActionResult> Manage()
		{
			ViewData["active"] = "product";
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> CreateProduct([FromQuery] int step = 1)
		{
			ViewBag.Step = step;
			if (step == 1)
			{
				var categories = await categoryService.GetSelectListCategories();
				var status = new List<ProductStatus>()
				{
					ProductStatus.Active,
					ProductStatus.Inactive
				};
				var features = new List<ProductFeatured>()
				{
					ProductFeatured.Yes,
					ProductFeatured.No,
				};
				CreateProductViewModel model = new CreateProductViewModel()
				{
					Categories = new SelectList(categories, nameof(SelectCategoryDTO.Id), nameof(SelectCategoryDTO.Name)),
					Status = new SelectList(status),
					Features = new SelectList(features)
				};
				return View(model);
			}
			else
			{
				
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(CreateProduct), 1);
			}

			return RedirectToAction(nameof(CreateProductVariant), 2);
		}

		[HttpGet]
		public IActionResult CreateProductVariant([FromQuery] int step = 2)
		{
			ViewBag.Step = step;
			CreateProductVariantViewModel model = new CreateProductVariantViewModel();
			return View(model);
		}

		[HttpPost]
		public IActionResult CreateProductVariant(CreateProductVariantViewModel model)
		{
			if (!ModelState.IsValid)
			{

			}
			return View(model);
		}

		#endregion
	}
}
