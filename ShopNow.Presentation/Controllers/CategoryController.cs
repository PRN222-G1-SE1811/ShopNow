using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Application.DTOs.Categories;
using ShopNow.Application.Services.Implements;
using ShopNow.Application.Services.Interfaces;

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

        public async Task<IActionResult> Manage(string? search, string? sortByDate)
        {
            var categories = await _categoryService.GetListCategories();

            // Gán tên danh mục cha ngay trong Manage()
            var categoryList = categories.Select(c => new ListCategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                ParentId = c.ParentId,
                ParentCategoryName = categories.FirstOrDefault(p => p.Id == c.ParentId)?.Name ?? "N/A",
                Slug = c.Slug,
                Description = c.Description,
                Image = c.Image,
                Status = c.Status,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            // Lọc theo tên nếu có tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                categoryList = categoryList
                    .Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Sắp xếp theo CreatedAt hoặc UpdatedAt
            categoryList = sortByDate switch
            {
                "CreatedAt" => categoryList.OrderByDescending(c => c.CreatedAt).ToList(),
                "UpdatedAt" => categoryList.OrderByDescending(c => c.UpdatedAt).ToList(),
                _ => categoryList
            };

            ViewData["active"] = "category";
            return View(categoryList);
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
            ViewBag.ParentCategories = await _categoryService.GetParentCategories();

            return View(new CreateCategoryDTO());
        }




        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO model)
        {
            if (!ModelState.IsValid)
            {
                var parentCategories = await _categoryService.GetParentCategories();
                ViewBag.ParentCategories = new SelectList(parentCategories, "Id", "Name");
                return View(model);
            }

            var result = await _categoryService.CreateCategory(model);

            if (result)
            {
                TempData["Success"] = "Category created successfully.";
                return RedirectToAction("Manage");
            }
            else
            {
                TempData["Error"] = "Failed to create category.";
                var parentCategories = await _categoryService.GetParentCategories();
                ViewBag.ParentCategories = new SelectList(parentCategories, "Id", "Name");
                return View(model);
            }
        }


    }
}
