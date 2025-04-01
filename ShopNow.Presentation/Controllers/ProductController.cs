using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.ProductViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Shared.Enums;
using ShopNow.Application.DTOs.Categories;
using ShowNow.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using ShopNow.Application.Services.Implements;

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

        public async Task<IActionResult> Manage(string? search, string? category, string? sortBy, int pageIndex = 1, int pageSize = 10)
        {
            // Lấy danh sách categories
            var categories = await categoryService.GetSelectListCategories();
            ViewBag.Categories = categories.Select(c => c.Name).ToList();

            Guid? categoryId = null;
            if (!string.IsNullOrEmpty(category))
            {
                var selectedCategory = categories.FirstOrDefault(c => c.Name == category);
                if (selectedCategory != null)
                {
                    categoryId = selectedCategory.Id;  
                }
            }

            // Gọi service với categoryId
            var (products, totalCount) = await productService.GetPaginatedAsync(
                pageIndex, pageSize, search, categoryId, null, null, sortBy, null
            );

            // Gán dữ liệu vào ViewModel
            var viewModel = new ProductManageViewModel
            {
                Products = products,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                PageIndex = pageIndex
            };

            return View(viewModel);
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

		[HttpGet]
		[Authorize(Roles = RoleName.Administrator)]
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
				Status = new SelectList(status),
				Features = new SelectList(features)
			};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = RoleName.Administrator)]
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
		[Authorize(Roles = RoleName.Administrator)]
		public IActionResult CreateProductVariant(Guid? productId)
		{
			if (productId == null) return RedirectToAction(nameof(CreateProduct), 1);
			ViewBag.Step = 2;
			CreateProductVariantViewModel model = new CreateProductVariantViewModel();
			model.ProductId = productId.Value;
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = RoleName.Administrator)]
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
