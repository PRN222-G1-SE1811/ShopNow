using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Application.DTOs.Categories;
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
            var categories = await _categoryService.GetSelectListCategories();

            // Gán tên danh mục cha ngay trong Manage()
            var categoryList = categories.Select(c => new SelectCategoryDTO
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


        // 2️⃣ Hiển thị trang tạo danh mục
        //public async Task<IActionResult> Create()
        //{
        //	var model = new CreateCategoryViewModel
        //	{
        //		Categories = new SelectList(await _categoryService.GetSelectListCategories(), "Id", "Name")
        //	};
        //	return View(model);
        //}

        //// 3️⃣ Xử lý tạo danh mục (POST)
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(CreateCategoryViewModel model)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		model.Categories = new SelectList(await _categoryService.GetSelectListCategories(), "Id", "Name");
        //		return View(model);
        //	}

        //	await _categoryService.CreateCategory(model);
        //	return RedirectToAction(nameof(Index));
        //}

        //// 4️⃣ Hiển thị trang chỉnh sửa danh mục
        //public async Task<IActionResult> Edit(int id)
        //{
        //	var category = await _categoryService.GetCategoryById(id);
        //	if (category == null)
        //	{
        //		return NotFound();
        //	}

        //	var model = new EditCategoryViewModel
        //	{
        //		Id = category.Id,
        //		Name = category.Name,
        //		Categories = new SelectList(await _categoryService.GetSelectListCategories(), "Id", "Name")
        //	};

        //	return View(model);
        //}

        //// 5️⃣ Xử lý cập nhật danh mục (POST)
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, EditCategoryViewModel model)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		model.Categories = new SelectList(await _categoryService.GetSelectListCategories(), "Id", "Name");
        //		return View(model);
        //	}

        //	await _categoryService.UpdateCategory(id, model);
        //	return RedirectToAction(nameof(Index));
        //}


    }
}
