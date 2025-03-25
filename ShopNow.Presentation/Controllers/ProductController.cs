using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.ProductViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Shared.Enums;
using System.Text.Json;
using ShopNow.Application.DTOs.Prodducts;
using System.Runtime.InteropServices.Marshalling;

namespace ShopNow.Presentation.Controllers
{
	public class ProductController(ICategoryService categoryService, IProductService productService, IProductVariantService productVariantService) : Controller
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

		public async Task<IActionResult> Create(int step = 0, Guid? productId = null)
		{
			ViewBag.Step = step;
			if (step == 0)
			{
				CreateProductBaseInfoViewModel model = new CreateProductBaseInfoViewModel();
				model.Categories = new SelectList(await categoryService.GetSelectListCategories(), "Id", "Name");
				model.Featured = new SelectList(new ProductFeatured[] { ProductFeatured.No, ProductFeatured.Yes });
				return View("CreateBaseInfo", model);
			}

			if (step == 1)
			{
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
				ViewBag.ProductId = productId;
				return View("CreateAttribute", model);
			}

			return RedirectToAction(nameof(Manage));
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProductBaseInfoViewModel model)
		{
			model.CreateProductDTO.CreatedAt = DateTime.Now;
			model.CreateProductDTO.Status = ProductStatus.MissingAttribute;
			var productId = await productService.CreateBaseProduct(model.CreateProductDTO);
			return RedirectToAction(nameof(Create), new { step = 1, productId = productId });
		}

		[HttpPost]
		public async Task<IActionResult> CreateAttribute(CreateAttributeViewModel model)
		{
			model.ProductVariantDTOs.ForEach(p =>
			{
			   p.ProductDetail.ProductId = model.ProductId;
			});

			await productVariantService.AddRangeVariantProduct(model.ProductVariantDTOs);

			return RedirectToAction(nameof(Create), new { step = 2 });
		}
	}
}
