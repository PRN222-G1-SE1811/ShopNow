using AutoMapper;
using ShopNow.Application.DTOs.Categories;
using ShopNow.Application.Services.Interfaces;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;
using System.Linq.Expressions;

namespace ShopNow.Application.Services.Implements
{
	public class CategoryService(IUnitOfWork<Category, Guid> unitOfWork, IMapper mapper) : ICategoryService
	{
		public async Task<IEnumerable<SelectCategoryDTO>> GetSelectListCategories(object? condition = null)
		{
			Expression<Func<Category, bool>> predicate = x => (
				condition == null || (Guid)condition == x.Id
			);
			var categories = await unitOfWork.GenericRepository.GetAllListAsync(predicate);
			return mapper.Map<IEnumerable<SelectCategoryDTO>>(categories);
		}
	}
}
