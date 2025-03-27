using AutoMapper;
using ShopNow.Application.DTOs.Prodducts;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Mappers
{
	public class ProductMapper : Profile
	{
		public ProductMapper()
		{
			CreateMap<CreateProductDTO, Product>();
			CreateMap<Product, ProductDetailDTO>()
				.ForMember(dest => dest.ProductVariants, opt => opt.MapFrom(src => src.ProductVariants));
		}
	}
}
