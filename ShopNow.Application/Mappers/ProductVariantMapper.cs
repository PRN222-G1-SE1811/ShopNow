using AutoMapper;
using ShopNow.Application.DTOs.Prodducts;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Mappers
{
	public class ProductVariantMapper : Profile
	{
		public ProductVariantMapper()
		{
			CreateMap<ProductAttributeDTO, ProductVariant>()
				.ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Quantity))
				.ForMember(dest => dest.ProductAssets, opt => opt.Ignore());
		}
	}
}
