using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Application.DTOs.Categories;

namespace ShopNow.Presentation.Models.CategoryViewModel
{
    public class CategoriesViewModels
    {
        public CreateCategoryDTO CreateCategoryDTO { get; set; } = new CreateCategoryDTO(); // Luôn khởi tạo tránh null

        public UpdateCategoryDTO? UpdateCategoryDTO { get; set; }

        public SelectList? ParentCategories { get; set; }
    }

}
