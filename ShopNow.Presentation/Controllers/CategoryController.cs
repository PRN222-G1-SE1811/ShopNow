using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Application.DTOs.Categories;
using ShopNow.Application.Services.Implements;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Presentation.Models.CategoryViewModel;

namespace ShopNow.Presentation.Controllers
{
	public class CategoryController : Controller
	{

		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Manage(string? search, string? sortByDate, int page = 1, int pageSize = 3)
        {
            var paginatedCategories = await _categoryService.GetPaginatedCategories(search, sortByDate, page, pageSize);

            ViewData["active"] = "category";
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = paginatedCategories.TotalPages;
            ViewData["CurrentSearch"] = search ?? "";
            ViewData["CurrentSort"] = sortByDate ?? "";

            return View(paginatedCategories.Items);
        }


        public async Task<IActionResult> Detail(Guid id)
        {
            Console.WriteLine($"📢 Nhận ID: {id}");

            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                Console.WriteLine($"❌ Không tìm thấy danh mục với ID: {id}");
                return NotFound();
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var parentCategories = await _categoryService.GetParentCategories();

            var viewModel = new CategoriesViewModels
            {
                ParentCategories = new SelectList(parentCategories, "Id", "Name")
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriesViewModels model)
        {
            if (!ModelState.IsValid)
            {
                model.ParentCategories = new SelectList(await _categoryService.GetParentCategories(), "Id", "Name");
                return View(model);
            }

            var result = await _categoryService.CreateCategory(model.CreateCategoryDTO);

            if (result)
            {
                TempData["Success"] = "Category created successfully.";
                return RedirectToAction("Manage");
            }

            TempData["Error"] = "Failed to create category.";
            model.ParentCategories = new SelectList(await _categoryService.GetParentCategories(), "Id", "Name");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _categoryService.GetCategoryUpdateById(id);
            if (category == null)
            {
                TempData["Error"] = "Category not found.";
                return RedirectToAction("Manage");
            }

            ViewBag.ParentCategories = await _categoryService.GetParentCategories();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ParentCategories = await _categoryService.GetParentCategories();
                return View(dto);
            }

            var result = await _categoryService.UpdateCategory(dto);
            if (result)
            {
                TempData["Success"] = "Category updated successfully.";
                return RedirectToAction("Manage");
            }

            TempData["Error"] = "Failed to update category.";
            ViewBag.ParentCategories = await _categoryService.GetParentCategories();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (!result)
            {
                TempData["Error"] = "Failed to delete category. It may not exist.";
            }
            else
            {
                TempData["Success"] = "Category deleted successfully.";
            }

            return RedirectToAction("Manage");
        }

    }
}
