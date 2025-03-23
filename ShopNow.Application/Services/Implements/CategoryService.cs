using AutoMapper;
using ShopNow.Application.DTOs.Categories;
using ShopNow.Application.Services.Interfaces;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;

namespace ShopNow.Application.Services.Implements
{
	public class CategoryService(IUnitOfWork<Category, Guid> unitOfWork, IMapper mapper) : ICategoryService
	{
		public async Task<IEnumerable<SelectCategoryDTO>> GetSelectListCategories()
		{
			var categories = await unitOfWork.GenericRepository.GetAllListAsync();
			return mapper.Map<IEnumerable<SelectCategoryDTO>>(categories);
		}
	}
}
