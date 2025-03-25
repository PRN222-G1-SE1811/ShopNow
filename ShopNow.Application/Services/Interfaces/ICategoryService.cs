using ShopNow.Application.DTOs.Categories;
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
        public Task<SelectCategoryDTO> GetCategoryById(Guid id);
    }
}
