using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.ProductViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Shared.Enums;
using ShopNow.Application.DTOs.Categories;
using ShowNow.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ShopNow.Presentation.Controllers
{
	public class ProductController(ICategoryService categoryService, IProductService productService, IProductVariantService productVariantService) : Controller
	{
		#region customer side

		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		[HttpGet("Product/{id:guid}")]
		public async Task<IActionResult> ProductDetail(Guid id)
		{
			var productDetail = await productService.GetProductDetail(id);
			ProductDetailViewModel model = new ProductDetailViewModel()
			{
				ProductDetailDTO = productDetail,
			};
			return View(model);
		}

		#endregion

		#region manage
		[Authorize(Roles = "ADMINISTRATOR")]
		public async Task<IActionResult> Manage()
		{
			ViewData["active"] = "product";
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "ADMINISTRATOR")]
		public async Task<IActionResult> CreateProduct([FromQuery] int step = 1)
		{
			if (step != 1) step = 1;
			ViewBag.Step = step;

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
				Statuses = new SelectList(status),
				Featured = new SelectList(features)
			};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "ADMINISTRATOR")]
		public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(CreateProduct), 1);
			}
			var productId = await productService.CreateProduct(model.CreateProductDTO);

			return RedirectToAction(nameof(CreateProductVariant), new { productId });
		}

		[HttpGet]
		[Authorize(Roles = "ADMINISTRATOR")]
		public IActionResult CreateProductVariant(Guid? productId)
		{
			if (productId == null) return RedirectToAction(nameof(CreateProduct), 1);
			ViewBag.Step = 2;
			CreateProductVariantViewModel model = new CreateProductVariantViewModel();
			model.ProductId = productId.Value;
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "ADMINISTRATOR")]
		public async Task<IActionResult> CreateProductVariant(CreateProductVariantViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(CreateProductVariant), new { model.ProductId });
			}

			var isCreated = await productVariantService.CreateProdductVariants(model.ProductId, model.ProductVariantDTOs);
			if (!isCreated)
			{
				return RedirectToAction(nameof(CreateProductVariant), new { model.ProductId });
			}
			return RedirectToAction(nameof(Manage));
		}
		#endregion
	}
}
