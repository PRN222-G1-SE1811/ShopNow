using AutoMapper;
using ShopNow.Application.DTOs.Categories;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Mappers
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<Category, SelectCategoryDTO>();
		}
	}
}
