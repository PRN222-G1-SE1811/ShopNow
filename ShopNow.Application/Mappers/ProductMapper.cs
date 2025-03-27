using AutoMapper;
using ShopNow.Application.DTOs.Products;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Mappers
{
	public class ProductMapper : Profile
	{
		public ProductMapper()
		{
			CreateMap<CreateProductDTO, Product>();
		}
	}
}
