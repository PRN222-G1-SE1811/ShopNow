using ShopNow.Application.DTOs.Categories;
using ShowNow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.Services.Interfaces
{
	public interface ICategoryService
	{
		public Task<IEnumerable<SelectCategoryDTO>> GetSelectListCategories();
		public Task<IEnumerable<ListCategoryDTO>> GetListCategories();
		public Task<ListCategoryDTO> GetCategoryById(Guid id);
		public Task<bool> CreateCategory(CreateCategoryDTO dto);
		public Task<List<SelectCategoryDTO>> GetParentCategories();
		public Task<bool> UpdateCategory(UpdateCategoryDTO dto);
		public Task<UpdateCategoryDTO> GetCategoryUpdateById(Guid id);
        public Task<bool> DeleteCategory(Guid id);


    }
}
