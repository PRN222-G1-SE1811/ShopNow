using AutoMapper;
using ShopNow.Application.DTOs.Products;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Mappers
{
	public class ProductVariantMapper : Profile
	{
		public ProductVariantMapper()
		{
			CreateMap<CreateProductVariantDTO, ProductVariant>()
			.ForMember(dest => dest.Assets, opt => opt.Ignore())
			.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
		}
	}
}
