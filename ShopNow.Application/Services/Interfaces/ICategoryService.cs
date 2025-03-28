using ShopNow.Application.DTOs.Categories;

namespace ShopNow.Application.Services.Interfaces
{
	public interface ICategoryService
	{
		public Task<IEnumerable<SelectCategoryDTO>> GetSelectListCategories(object? condition = null);
	}
}
