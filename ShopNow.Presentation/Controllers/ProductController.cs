using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.ProductViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Shared.Enums;
using System.Text.Json;
using ShopNow.Application.DTOs.Prodducts;

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

		public async Task<IActionResult> Create(int step = 0)
		{
			ViewBag.Step = step; // Truyền bước hiện tại qua View
			if (step == 0)
			{
				CreateProductBaseInfoViewModel model = new CreateProductBaseInfoViewModel();
				model.Categories = new SelectList(await categoryService.GetSelectListCategories(), "Id", "Name");
				model.Featured = new SelectList(new ProductFeatured[] { ProductFeatured.No, ProductFeatured.Yes });
				return View("CreateBaseInfo", model);
			}

			if (step == 1)
			{
				//var baseInfo = JsonSerializer.DeserializeObject<CreateProductBaseInfoViewModel>((string)TempData["BaseInfo"]);
				//TempData.Keep("BaseInfo"); // Giữ lại dữ liệu
				CreateAttributeViewModel model = new CreateAttributeViewModel();
				model.ProductVariantDTOs = new List<ProductVariantDTO>()
				{
					new ProductVariantDTO() {
						AttributeDTOs = new List<AttributeDTO>()
						{
							new AttributeDTO()
						},
						ProductDetail = new ProductAttributeDTO()
					}
				};

				return View("CreateAttribute", model);
			}

			return RedirectToAction(nameof(Manage));
		}

		[HttpPost]
		public IActionResult Create(CreateProductBaseInfoViewModel model)
		{
			return RedirectToAction(nameof(Create), new { step = 1 });
		}

		[HttpPost]
		public IActionResult CreateAttribute(CreateAttributeViewModel model)
		{
			return RedirectToAction(nameof(Create), new { step = 2 });
		}
	}
}
