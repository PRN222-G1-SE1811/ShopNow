using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.ProductViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Shared.Enums;
using ShopNow.Application.DTOs.Categories;
using ShowNow.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using ShopNow.Application.Services.Implements;
using ShopNow.Application.DTOs.Products;
using NuGet.ContentModel;

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
        public async Task<IActionResult> Shop()
        {
			var product = await productService.GetAllProductsAsync();
            return View(product);
        }


        //public async Task<IActionResult> Manage(string? search, string? category, string? sortBy, int pageIndex = 1, int pageSize = 10)
        //{
        //    // Lấy danh sách categories
        //    var categories = await categoryService.GetSelectListCategories();
        //    ViewBag.Categories = categories.Select(c => c.Name).ToList();

		[HttpGet]
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





  //      [AllowAnonymous]
		//[HttpGet("Product/{id:guid}")]
		//public async Task<IActionResult> ProductDetail(Guid id)
		//{
		//	var productDetail = await productService.GetProductDetail(id);

		//	if (productDetail == null)
		//	{
		//		// Nếu không có dữ liệu sản phẩm, có thể trả về 404 hoặc trang lỗi
		//		return NotFound();
		//	}

		//	ProductDetailViewModel model = new ProductDetailViewModel()
		//	{
		//		ProductDetailDTO = productDetail,
		//	};

		//	return View(model);
		//}

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

        [HttpGet]
        public async Task<IActionResult> EditProduct(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["Error"] = "Invalid product ID.";
                return RedirectToAction(nameof(Manage));
            }

            var product = await productService.GetProductEditById(id);
            if (product == null)
            {
                TempData["Error"] = "Product not found.";
                return RedirectToAction(nameof(Manage));
            }

            var categories = await categoryService.GetSelectListCategories();
            var status = new List<ProductStatus> { ProductStatus.Active, ProductStatus.Inactive };
            var features = new List<ProductFeatured> { ProductFeatured.Yes, ProductFeatured.No };

            var model = new EditProductViewModel
            {
                EditProductDTO = product,
                Categories = new SelectList(categories, "Id", "Name", product.CategoryID),
                Status = new SelectList(status, product.Status),
                Features = new SelectList(features, product.Featured)
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Nếu dữ liệu không hợp lệ, quay lại trang chỉnh sửa và hiển thị lỗi
                return View(model);
            }

            // Gọi service để cập nhật sản phẩm
            var isUpdated = await productService.EditProduct(model.EditProductDTO);

            if (!isUpdated)
            {
                // Nếu việc cập nhật không thành công, hiển thị thông báo lỗi
                TempData["Error"] = "Product update failed. Please try again.";
                return View(model);  // Quay lại trang chỉnh sửa
            }

            // Redirect đến trang quản lý sản phẩm sau khi cập nhật thành công
            TempData["Success"] = "Product updated successfully!";
            return RedirectToAction("Manage");
        }

        // GET: Product/Delete/{id}
        public IActionResult Delete(Guid id)
        {
            // Tạo view Delete với thông tin sản phẩm
            return View(id);
        }

        // POST: Product/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                bool result = await productService.DeleteProduct(id);

                if (result)
                {
                    // Nếu xóa thành công, chuyển hướng đến trang danh sách sản phẩm
                    TempData["SuccessMessage"] = "Sản phẩm đã được xóa thành công.";
                    return RedirectToAction("Manage");
                }
                else
                {
                    // Nếu không thể xóa (ví dụ sản phẩm không tồn tại)
                    TempData["ErrorMessage"] = "Không thể xóa sản phẩm. Sản phẩm không tồn tại.";
                    return RedirectToAction("Manage");
                }
            }
            catch (InvalidOperationException ex)
            {
                // Nếu có lỗi liên quan đến ProductVariant, trả về thông báo lỗi
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                TempData["ErrorMessage"] = "Đã có lỗi xảy ra khi xóa sản phẩm.";
                return RedirectToAction("Manage");
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditProductVariant(Guid id)
        {
            // Lấy thông tin biến thể sản phẩm
            var variant = await productVariantService.GetProductVariantByIdAsync(id);
            if (variant == null)
            {
                return NotFound();
            }

            // Lấy các ảnh cũ từ ProductVariant
            var existingAssets = await productVariantService.GetAssetsByProductVariantIdAsync(id);

            // Map dữ liệu vào ViewModel
            var editViewModel = new EditProductVariantViewModel
            {
                ProductVariantDTOs = new List<EditProductVariantDTO>
        {
            new EditProductVariantDTO
            {
                Id = variant.Id,
                Discount = (decimal)variant.Discount,
                Quantity = variant.Quantity,
                Color = variant.Color,
                Size = variant.Size
            }
        }
            };

            // Truyền danh sách ảnh cũ vào ViewBag
            ViewBag.ExistingAssets = existingAssets;
            ViewBag.ProductId = variant.ProductId;  // Truyền ProductId vào ViewBag

            return View(editViewModel);
        }



        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> EditProductVariant(Guid id, EditProductVariantViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool allSuccess = true;
                foreach (var dto in viewModel.ProductVariantDTOs)
                {
                    // Sửa biến thể sản phẩm theo dto (ProductVariantDTO)
                    var result = await productVariantService.EditProductVariantAsync(dto);
                    if (!result)
                    {
                        ModelState.AddModelError("", "Product variant not found or failed to update.");
                        allSuccess = false;
                    }
                }

                if (allSuccess)
                {
                    TempData["SuccessMessage"] = "Chỉnh sửa thành công.";

                    // Lấy URL của trang trước đó
                    var refererUrl = Request.Headers["Referer"].ToString();

                    if (!string.IsNullOrEmpty(refererUrl))
                    {
                        return Redirect(refererUrl); // Redirect về trang trước đó
                    }

                    // Nếu không có Referer header, chuyển hướng về trang chi tiết sản phẩm
                    return RedirectToAction("ProductDetail", "Product", new { id = viewModel.ProductVariantDTOs.First().Id });
                }
            }

            return View(viewModel);
        }



        [HttpGet]
        public async Task<IActionResult> DeleteVariant(Guid id)
        {
            // Lấy thông tin biến thể sản phẩm cần xóa
            var variant = await productVariantService.GetProductVariantByIdAsync(id);
            if (variant == null)
            {
                return NotFound();
            }

            return View(variant); // Trả về View để người dùng xác nhận trước khi xóa
        }

        [HttpPost]
        [ActionName("DeleteVariant")]
        public async Task<IActionResult> DeleteVariantConfirmed(Guid id)
        {
            var result = await productVariantService.DeleteProductVariantAsync(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Variant deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete variant.";
            }

            // Trả về trang trước đó (Lấy URL từ Referer header)
            var refererUrl = Request.Headers["Referer"].ToString();

            // Kiểm tra nếu có URL trước đó và chuyển hướng về đó
            if (!string.IsNullOrEmpty(refererUrl))
            {
                return Redirect(refererUrl);
            }

            // Nếu không có URL, mặc định sẽ quay lại trang chi tiết sản phẩm
            return RedirectToAction("ProductDetail", "Product", new { id = id });
        }






    }
}
