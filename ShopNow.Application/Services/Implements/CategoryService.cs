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

        public async Task<SelectCategoryDTO> GetCategoryById(Guid id)
        {
            var categories = await unitOfWork.GenericRepository.GetAllListAsync();
            var category = categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            return new SelectCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                ParentCategoryName = categories.FirstOrDefault(p => p.Id == category.ParentId)?.Name ?? "N/A",
                Slug = category.Slug,
                Description = category.Description,
                Image = category.Image,
                Status = category.Status,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }
    }
}
